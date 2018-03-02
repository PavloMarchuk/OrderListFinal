USE Master
GO
IF DB_ID('KyivgazDB') IS NOT NULL
  DROP DATABASE KyivgazDB;
 GO

 CREATE DATABASE KyivgazDB collate Ukrainian_CI_AI
 GO
 USE KyivgazDB;
  GO

   CREATE TABLE Manager
 (
   Id int identity primary key,
   LastName nvarchar(64) not null
 )
 SET IDENTITY_INSERT Manager ON
 INSERT INTO Manager(Id,LastName)
 VALUES
 (1, 'МЕЛЬНИК'),
 (2, 'ШЕВЧЕНКО'),
 (3, 'БОЙКО'),
 (4, 'КОВАЛЕНКО'),
 (5, 'БОНДАРЕНКО'),
 (6, 'ТКАЧЕНКО'),
 (7, 'КОВАЛЬЧУК');
 SET IDENTITY_INSERT Manager OFF
 

 CREATE TABLE Invoice
 (
   Id int identity primary key,
   InvoiceNumber nvarchar(64) not null,
   DateCreated DateTime not null,
   DateOfShipment  DateTime null,
   ManagerId int foreign key REFERENCES Manager(Id) not null,
   Annotation nvarchar(512) null
 )
 GO

 SET IDENTITY_INSERT Invoice ON
 INSERT INTO Invoice(Id, InvoiceNumber,DateCreated, DateOfShipment,ManagerId, Annotation)
 VALUES
 (1,'ТТН-0001',  GETDATE(), null, 1, 'нотатка1'),
 (2,'ТТН-0002', GETDATE(), null, 1, ''),
 (3,'ТТН-0003', GETDATE(), GETDATE(), 1, ''),
 (4,'ТТН-0004', GETDATE(), null, 2, ''),
 (5,'ТТН-0005', GETDATE(), null, 2, 'нотатка5'),
 (6,'ТТН-0006', GETDATE(), null, 1, ''),
 (7,'ТТН-0007', GETDATE(), null, 4, ''),
 (8,'ТТН-0008', GETDATE(), null, 1, ''),
 (9,'ТТН-0009', '2007-04-20', GETDATE(), 1, 'нотатка9'),
 (10,'ТТН-0010', '2007-04-30', '2007-05-30', 7, ''),
 (11,'ТТН-0011', '2007-05-03', null, 2, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (12,'ТТН-0011', '2007-05-03', null, 2, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (13,'ТТН-0011', '2007-05-03', null, 2, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (14,'ТТН-0011', '2007-05-03', null, 1, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (15,'ТТН-0011', '2007-05-03', null, 1, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (16,'ТТН-0011', '2007-05-03', null, 1, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.');
 SET IDENTITY_INSERT Invoice OFF 


 --Обязательно отдельно предоставить SQL-запрос подсчета количества заказов по менеджерам за период. В запросе учесть и тех менеджеров, у которых в выбранном периоде заказов нет.



Select  m.LastName AS працівник, m.Id AS працівникID , COUNT( invc.ManagerId) AS "количества заказов по менеджерам за период" 
FROM Manager as m
	LEFT JOIN Invoice AS invc ON m.Id = invc.ManagerId
	and DateCreated Between '2000-07-13' AND GETDATE()
	 GROUP BY m.LastName, m.Id


