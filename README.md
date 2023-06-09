# Payment_Calculator

<b>Вводная:</b>
Существует бухгалтерская система, основной сущностью в ней является кредит, доступ разработчика к которому осуществляется через ILoan. К каждому кредиту привязан набор счетов различного типа (AccountType). К данным счетам у разработчика доступа нет. Любое изменение состояния счетов и кредитов происходят автоматически.
Все изменения балансов счетов производятся при помощи операций, доступ разработчика к которому осуществляется через интерфейс IOperation.
Если некоторое действие предполагает перевод средств с одного счета на другой, то создается 2 операции (списание на счете A и начисление на счет B).
Все DateTime хранятся без времени.
Когда пользователь берет кредит, то создается график платежей (IPlannedPayment), в
котором указаны:
- PaymentDate - Дата платежа
- BaseDebt - Сумма, необходимая для погашения основного долга
- Interest - Сумма, необходимая для погашения начисленных процентов
- RemainingBaseDebt - Остаток по основному долгу, который будет после платежа.
В самом конце дня происходит процесс закрытия/открытия дня, во время которого обновляются все балансы на счетах активных кредитов. А также происходит перевод статуса кредита из “Нормальный” в "Просрочен" и обратно.
Пользователь может вносить различные суммы денег, которые автоматически разбиваются и распределяются по разным счетам. Сумма может быть как меньше платежа по графику, так и больше, но не превышая общую сумму задолженности. При этом может платить в любой день, не только в день платежа по графику. Если к концу дня платежа по графику клиент не оплатил необходимую сумму, то кредит становится просроченным, пока данный долг не будет уплачен.

<b>Задача:</b>
Написать rest api с двумя GET-методами:
1. Full Payment. Принимает id кредита и возвращает объект, в котором будет возвращаться
сумму, достаточную для того, чтобы полностью закрыть кредит.
А также должна быть разбивка этой суммы, сколько и на какой счет должно быть перечислено.
Объект должен состоять из полей:
- BaseDebt - Interest
- Penalty
- Total
Необходимо проверять все остатки на счетах, и учитывать, что средства могут быть внесены заранее или кредит может быть в просрочке.
Если кредит закрыт, то значения всех полей = 0.
2. Partial Payment. Принимает id кредита и возвращает объект, в котором будет возвращаться сумма, которую, если клиент внесет сегодня, то в ближайший день платежа балансов на счету будет достаточно для того, чтобы задолженность по графику была погашена автоматически.
Также должна быть разбивку этой суммы, сколько и на какой счет должно быть перечислено (как в задаче 1)
Идея заключается в том, что если клиент внесет эту сумму, то сумма его основного будет заранее уменьшена, а следовательно процентов до дня платежа будет начислено меньше, чем указано в платеже графику.
Возвращает все по 0, если клиент уже внес такой платеж в этом месяце.
