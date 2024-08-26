#include "deneyap.h"
#include <WiFi.h>
#include <WebServer.h>
#include <ESPmDNS.h>
#include "MAX30105.h"
#include "heartRate.h"

MAX30105 particleSensor;
WebServer server(80);

const char* ssid = "Galaxy A30";
const char* password = "isys7294";
const char* deviceName = "esp32"; // mDNS için cihaz adı

void setup() {
  Serial.begin(115200);
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("WiFi'ye bağlanılıyor...");
  }

  Serial.println("WiFi'ye bağlandı!");
  Serial.print("IP adresi: ");
  Serial.println(WiFi.localIP());

  if (!MDNS.begin(deviceName)) {
    Serial.println("mDNS başlatılamadı.");
    return;
  }

  Serial.println("mDNS başlatıldı.");
  Serial.print("mDNS adı: ");
  Serial.print(deviceName);
  Serial.println(".local");

  if (!particleSensor.begin(Wire, I2C_SPEED_FAST)) {
    Serial.println("MAX30102 sensörü bulunamadı. Lütfen bağlantıları kontrol edin.");
    while (1);
  }

  particleSensor.setup();
  particleSensor.setPulseAmplitudeRed(0x0A); // Kırmızı LED'i düşük seviyeye ayarlayın
  particleSensor.setPulseAmplitudeGreen(0); // Yeşil LED'i kapatın

  server.on("/start", HTTP_GET, handleStart);
  server.begin();
  Serial.println("Web sunucusu başlatıldı.");
}

void loop() {
  Serial.println("loop'a girildi.");
  server.handleClient();
  Serial.println("server.handleClient() komutu çalıştı.");
}

void handleStart() {
  Serial.println("handleStart()'a girildi.");
  int measurementTime = 10000; // 10 saniye
  unsigned long startTime = millis();
  long irValue;
  int beatsDetected = 0;
  long sumBeats = 0;
  unsigned long lastBeat = 0;

  while (millis() - startTime < measurementTime) {
    Serial.println("Ölçüm süresi bitmedi, lütfen bekleyiniz.");
    irValue = particleSensor.getIR();

    if (checkForBeat(irValue)) {
      Serial.println("Parmak tespit edildi.");
      float beatsPerMinute = 60 / ((millis() - lastBeat) / 1000.0);
      lastBeat = millis();
      if (beatsPerMinute > 20 && beatsPerMinute < 255) {
        Serial.println("Anlık nabız başarılı olarak tespit edildi.");
        sumBeats += beatsPerMinute;
        beatsDetected++;
      }
    }
    delay(20);
  }
  Serial.println("Ölçüm sona erdi.");
  
  if (beatsDetected > 0) {
    int averageBPM = sumBeats / beatsDetected;
    server.send(200, "text/plain", String(averageBPM));
    Serial.println("Nabız verisi server'a gönderildi.");
  } else {
    server.send(200, "text/plain", "Hata: Nabız algılanamadı.");
    Serial.println("Nabız tespit edilemedi.");
  }
}
