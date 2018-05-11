# Parking
The program _Parking simulator_ should have the following functional:
* Add / remove the car with parking.
* Refill the balance of the machine.
* Write off the funds for the parking space (every N-seconds will trigger the timer and write off each car parking costs).
* Display transaction history for the last minute.
* Derive the total parking revenue.
* Display the number of available parking spaces.
* Every minute, write down the Transactions.log transaction amount for the last minute with a date stamp.
* Print Transactions.log (format output)

# General logic:
* There must be only 1 parking in the program. When you initialize the parking, you must use the Settings class. Parking keeps a list of    cars / transactions.
* You can add a car to the parking lot or pick up from the parking lot. Every N-second parking charges money from the car. If the car does  not have enough money to pay parking, then write off the penalty (the penalty coefficient * the parking price). Also, we can not pick up the car until we replenish the balance and repeat the operation.
* When debiting funds, parking keeps transactions for the last minute. At any time, we can turn to the parking lot and find out the current balance (earned money). We can apply to the parking lot and find out the amount of money earned in the last minute. We can apply to the parking lot and find out the number of free / occupied places in the parking lot. The cost of parking depends on the type of car.
