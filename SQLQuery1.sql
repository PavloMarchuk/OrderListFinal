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
 (1, '�������'),
 (2, '��������'),
 (3, '�����'),
 (4, '���������'),
 (5, '����������'),
 (6, '��������'),
 (7, '���������');
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
 (1,'���-0001',  GETDATE(), null, 1, '�������1'),
 (2,'���-0002', GETDATE(), null, 1, ''),
 (3,'���-0003', GETDATE(), GETDATE(), 1, ''),
 (4,'���-0004', GETDATE(), null, 2, ''),
 (5,'���-0005', GETDATE(), null, 2, '�������5'),
 (6,'���-0006', GETDATE(), null, 1, ''),
 (7,'���-0007', GETDATE(), null, 4, ''),
 (8,'���-0008', GETDATE(), null, 1, ''),
 (9,'���-0009', '2007-04-20', GETDATE(), 1, '�������9'),
 (10,'���-0010', '2007-04-30', '2007-05-30', 7, ''),
 (11,'���-0011', '2007-05-03', null, 2, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (12,'���-0011', '2007-05-03', null, 2, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (13,'���-0011', '2007-05-03', null, 2, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (14,'���-0011', '2007-05-03', null, 1, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (15,'���-0011', '2007-05-03', null, 1, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.'),
 (16,'���-0011', '2007-05-03', null, 1, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit.');
 SET IDENTITY_INSERT Invoice OFF 


 --����������� �������� ������������ SQL-������ �������� ���������� ������� �� ���������� �� ������. � ������� ������ � ��� ����������, � ������� � ��������� ������� ������� ���.



Select  m.LastName AS ���������, m.Id AS ���������ID , COUNT( invc.ManagerId) AS "���������� ������� �� ���������� �� ������" 
FROM Manager as m
	LEFT JOIN Invoice AS invc ON m.Id = invc.ManagerId
	and DateCreated Between '2000-07-13' AND GETDATE()
	 GROUP BY m.LastName, m.Id


