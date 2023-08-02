#include <microDS18B20.h>     // Подключаем библиотеку работы с датчиков DS18B20

#define DS_PIN  4             // Датчик DS18B20 подключаем к выводу 4

/*== Массив, в котором храним адреса датчиков*/
uint8_t address_read[8];       // Массив для чтения адреса датчика
uint8_t address_ds1[8];       // Массив хранения адреса первого датчика
uint8_t address_ds2[8];       // Массив хранения адреса второго датчика
uint8_t address_ds3[8];       // Массив хранения адреса третьего датчика
uint8_t address_ds4[8];       // Массив хранения адреса четвертого датчика
uint8_t address_ds5[8];       // Массив хранения адреса пятого датчика
uint8_t address_ds6[8];       // Массив хранения адреса шестого датчика
uint8_t address_ds7[8];       // Массив хранения адреса седьмого датчика
uint8_t address_ds8[8];       // Массив хранения адреса восьмого датчика


MicroDS18B20 <DS_PIN> sensor;  // Создаем термометр без адреса на пине D4
MicroDS18B20 <DS_PIN, address_ds1> sensor1; // Датчик с адресом 1
MicroDS18B20 <DS_PIN, address_ds2> sensor2; // Датчик с адресом 2
MicroDS18B20 <DS_PIN, address_ds3> sensor3; // Датчик с адресом 3
MicroDS18B20 <DS_PIN, address_ds4> sensor4; // Датчик с адресом 4
MicroDS18B20 <DS_PIN, address_ds5> sensor5; // Датчик с адресом 5
MicroDS18B20 <DS_PIN, address_ds6> sensor6; // Датчик с адресом 6
MicroDS18B20 <DS_PIN, address_ds7> sensor7; // Датчик с адресом 7
MicroDS18B20 <DS_PIN, address_ds8> sensor8; // Датчик с адресом 8

/*Опредедение констант и переменных*/
String cmd;     // Переменная. Хранит команду, получаемую от ПК

/* Настройки*/
void setup() {
  Serial.begin(9600);
  Serial.setTimeout(50);
}

/*Основной цикл программы*/
void loop() {

  if (Serial.available()) {         // Если что либо есть в буфере приема COM порта
    cmd = Serial.readString();      // Читаем команду
    cmd.trim();                     // Удаляем лишние пробелы

    if (cmd == "GetTemp") {         // Если пришедшая команда "GetTemp"
      sensor1.requestTemp();        // Посылаем датчикам команду "начать преобразование температуры
      sensor2.requestTemp();
      sensor3.requestTemp();
      sensor4.requestTemp();
      sensor5.requestTemp();
      sensor6.requestTemp();
      sensor7.requestTemp();
      sensor8.requestTemp();

      delay(1000);                  // Ожидаем окончания преобразования темпертуры датчиками температуры
      
      SerialPrintResult();          // см. ниже
    }

    if (cmd == "GetAdr") {
      if (sensor.readAddress(address_read)) {
        for (int i = 0; i < 8; i++) {
          Serial.print(address_read[i]);
          if (i < 7) {
            Serial.print(":");
          }
        }
      } else {
        Serial.println("Not connected");
      }
    }
  }
}


/* Получаем значения температуры из датчиков и отправляем их на печать в COM порт */
void SerialPrintResult () {
  Serial.print(sensor1.getTemp());  // Получаем значение температуры датчика 1 и печатаем его в порт
  Serial.print("End1");       // Посылаем ключевое слово окончания данных температуры первого датчика
  Serial.print(sensor2.getTemp());  // Получаем значение температуры датчика 2 и печатаем его в порт
  Serial.print("End2");       // Посылаем ключевое слово окончания данных температуры второго датчика
  Serial.print(sensor3.getTemp());  // Получаем значение температуры датчика 3 и печатаем его в порт
  Serial.print("End3");       // Посылаем ключевое слово окончания данных температуры третьего датчика
  Serial.print(sensor4.getTemp());  // Получаем значение температуры датчика 4 и печатаем его в порт
  Serial.print("End4");       // Посылаем ключевое слово окончания данных температуры четвертого датчика
  Serial.print(sensor5.getTemp());  // Получаем значение температуры датчика 5 и печатаем его в порт
  Serial.print("End5");       // Посылаем ключевое слово окончания данных температуры пятого датчика
  Serial.print(sensor6.getTemp());  // Получаем значение температуры датчика 6 и печатаем его в порт
  Serial.print("End6");       // Посылаем ключевое слово окончания данных температуры шестого датчика
  Serial.print(sensor7.getTemp());  // Получаем значение температуры датчика 7 и печатаем его в порт
  Serial.print("End7");       // Посылаем ключевое слово окончания данных температуры седьмого датчика
  Serial.print(sensor8.getTemp());  // Получаем значение температуры датчика 8 и печатаем его в порт
  Serial.print("End8");       // Посылаем ключевое слово окончания данных температуры воьмого датчика
}
