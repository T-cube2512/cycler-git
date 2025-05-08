const int ir_pin = 2;
int rpm = 1;
unsigned long long int prev_time =1;
unsigned int count;

void setup() {
  // put your setup code here, to run once:
  attachInterrupt(digitalPinToInterrupt(ir_pin), count_times, RISING);

  Serial.begin(57600);
}

void loop() {

  if(millis() - prev_time > 1000)
  {
    
    Serial.println(rpm);
    rpm = count; 
    count = 0;
    //Serial.println(rpm);

    prev_time = millis();
    //Serial.println(rpm);

    //prev_time = millis();
  }

}

void count_times()
{
  count++;
  delay(10);
}