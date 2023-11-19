BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Discount" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"value"	NUMERIC NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Hall" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"hall number"	NUMERIC NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Lesson programs" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"program duration"	NUMERIC NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Status" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"title"	TEXT NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Subscription type" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"cost"	NUMERIC NOT NULL,
	"duration"	NUMERIC NOT NULL,
	"number of classes"	NUMERIC NOT NULL,
	"date and time of purchase"	NUMERIC NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Product" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"cost"	NUMERIC NOT NULL,
	"quantity"	NUMERIC NOT NULL,
	"expiration date"	NUMERIC NOT NULL,
	"ID product category"	INTEGER NOT NULL,
	FOREIGN KEY("ID product category") REFERENCES "Product category"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Subscription" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"validity start date"	NUMERIC NOT NULL,
	"validity expiration date"	NUMERIC NOT NULL,
	"ID trainer"	INTEGER NOT NULL,
	"ID client"	INTEGER NOT NULL,
	"ID status"	INTEGER NOT NULL,
	"ID subscription type"	INTEGER NOT NULL,
	FOREIGN KEY("ID trainer") REFERENCES "Position"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("ID status") REFERENCES "Status"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("ID client") REFERENCES "Client"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("ID subscription type") REFERENCES "Subscription type"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Employee" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"surname"	TEXT NOT NULL,
	"middle name"	TEXT,
	"date of birth"	NUMERIC NOT NULL,
	"gender"	NUMERIC NOT NULL,
	"length of service"	NUMERIC NOT NULL,
	"login"	TEXT NOT NULL UNIQUE,
	"password"	TEXT NOT NULL UNIQUE,
	"ID position"	INTEGER NOT NULL,
	FOREIGN KEY("ID position") REFERENCES "Position"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Client" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"secondname"	TEXT NOT NULL,
	"middle name"	TEXT,
	"date of birth"	NUMERIC NOT NULL,
	"login"	TEXT NOT NULL UNIQUE,
	"password"	TEXT NOT NULL UNIQUE,
	"ID discount"	TEXT NOT NULL,
	FOREIGN KEY("ID discount") REFERENCES "Discount"("ID") ON UPDATE CASCADE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Gym" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"start time"	NUMERIC NOT NULL,
	"end time"	NUMERIC NOT NULL,
	"adress"	TEXT NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Position" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"title"	TEXT NOT NULL,
	"salary"	NUMERIC NOT NULL,
	"work schedule"	NUMERIC NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Product category" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Lesson" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"date and time"	INTEGER NOT NULL,
	"ID trainer"	INTEGER NOT NULL,
	"ID hall"	INTEGER NOT NULL,
	"ID subscription"	INTEGER NOT NULL,
	"ID program"	INTEGER NOT NULL,
	"ID gym"	INTEGER NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT),
	FOREIGN KEY("ID program") REFERENCES "Lesson programs"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("ID gym") REFERENCES "Gym"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("ID trainer") REFERENCES "Position"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("ID subscription") REFERENCES "Subscription"("ID") ON UPDATE CASCADE,
	FOREIGN KEY("ID hall") REFERENCES "Hall"("ID") ON UPDATE CASCADE
);
INSERT INTO "Discount" ("ID","name","value") VALUES (1,'Новичок',15),
 (2,'Продвинутый',25),
 (3,'Профессионал',30);
INSERT INTO "Hall" ("ID","hall number") VALUES (1,2),
 (2,12),
 (3,22),
 (4,3),
 (5,1),
 (6,10),
 (7,7);
INSERT INTO "Lesson programs" ("ID","name","description","program duration") VALUES (1,'Растяжка','система упражнений, разработанная для укрепления мышц кора (силовых центров тела), улучшения гибкости и осанки. Он сфокусирован на правильном дыхании, контроле движений и концентрации, что помогает развить силу и гармоничное тело.',2),
 (2,'Силовая тренировка','Программа силовых тренировок предлагает набор упражнений, основанных на использовании гантелей, штанги и других силовых тренажеров. Она способствует развитию мышц, укреплению костной ткани, повышению выносливости и общего физического состояния.',2),
 (3,'Индивидульная тренировка','У вас будет личный тренер который расскажет как правильно делать упражнения интересующие вас больше всего',2),
 (4,'Йога','Йога представляет собой комплекс упражнений, направленных на развитие гибкости, баланса, силы и способности к расслаблению. Программа включает в себя асаны (физические позы), дыхательные и медитативные упражнения, которые помогают укрепить тело и улучшить психическое состояние',2),
 (5,'Пилатес','Пилатес - это система упражнений, разработанная для укрепления мышц кора (силовых центров тела), улучшения гибкости и осанки. Он сфокусирован на правильном дыхании, контроле движений и концентрации, что помогает развить силу и гармоничное тело',2),
 (6,'Кардиотренировки','Кардиотренировки направлены на укрепление сердечно-сосудистой системы и улучшение ее работоспособности. Включает в себя такие упражнения, как беговая дорожка, велотренажер, степ-платформа и т. д., которые помогают сжигать калории, улучшают кровообращение и укрепляют легкие',2);
INSERT INTO "Status" ("ID","title") VALUES (1,'Активен'),
 (2,'Не активен');
INSERT INTO "Subscription type" ("ID","name","cost","duration","number of classes","date and time of purchase") VALUES (1,'все включено',24500,6,100,'12:00 12.12.2022'),
 (2,'8 посещений',5000,2,8,'15:55 23.03.2023'),
 (3,'12 посещений',7000,2,12,'12:04 12.10.2022'),
 (4,'5 посещений',2400,2,5,'17:34 22.07.2022'),
 (5,'10 посещений',6500,2,10,'14:45 06.11.2023');
INSERT INTO "Product" ("ID","name","cost","quantity","expiration date","ID product category") VALUES (1,'Вода',89,1,'01.1.2028',1),
 (2,'Протеин',2400,1,'04.12.2025',1),
 (3,'Шорты',3000,1,'12.12.2040',2),
 (4,'Носки',1000,3,'03.12.2045',2),
 (5,'Батончик бомбар',109,1,'10.12.2024',1),
 (6,'Повязка на голову',2560,1,'03.12.2045',2);
INSERT INTO "Subscription" ("ID","validity start date","validity expiration date","ID trainer","ID client","ID status","ID subscription type") VALUES (1,'12:12 02.12.2022','10.00 03.02.2023',1,1,1,3),
 (2,'02.12.2023','10.00 03.02.2024',1,2,1,4),
 (3,'12.12.2020','01.02.2021',1,5,2,2),
 (4,'13.04.2022','21.05.2022',1,3,2,1),
 (5,'31.12.2022','04.03.2023',1,6,2,5),
 (6,'23.06.2023','25.07.2023',1,4,1,2);
INSERT INTO "Employee" ("ID","name","surname","middle name","date of birth","gender","length of service","login","password","ID position") VALUES (1,'Вячеслав','Бутыркин','Олегович','03.05.1999','Мужской',5,'акреоо4','аукперере7655',1),
 (2,'Анастасия','Филинкина','Османовна','06.12.1997','Женский',3,'вавапрппр6','ывв234354',5),
 (3,'Елена','Кукушина','Владимировна','14.03.1984','Женский',10,'выпарное6','3345564',3),
 (4,'Евгений','Аександров','Алексеевич','09.10.1979','Мужской',15,'ыфвапира6','авпрер6',6),
 (5,'Леонид','Пантелеймонов','Платонович','01.12.1985','Мужской',12,'впарпа4','вц45464',4),
 (6,'Виолетта','Девочкина','Захаровна','04.11.2000','Женский',4,'укаронг6','3467888',2);
INSERT INTO "Client" ("ID","name","secondname","middle name","date of birth","login","password","ID discount") VALUES (1,'Александр','Лопаткин','Егорович','20.02.1999','kgjns','876','2'),
 (2,'Ангелина','Малинкина','Валерьевна','14.12.1999',' итрпа44','23334321','1'),
 (3,'Аркадий','Паровозов','Максимович','03.03.2000','аролвдлв5','233444','3'),
 (4,'Владимир','Путин','Владимирович','08.10.1978','акеренер4','апрн3456','3'),
 (5,'Мария','Фетисова','Константиновна','06.06.2005','ваполг8','ввуаакак','2'),
 (6,'Елена','Кирилина','Васильевна','20.04.1995','аппрррррррр','ввввв445','3');
INSERT INTO "Gym" ("ID","name","start time","end time","adress") VALUES (1,'Deja vu','08:00','23:00','ул. Константинопольская 34'),
 (2,'Deja vu','08:00','23:00','ул. Полянская 45/1'),
 (3,'Deja vu','08:00','23:00','ул. Солонская 2'),
 (4,'Deja vu','08:00','23:00','ул. Березная 12'),
 (5,'Deja vu','08:00','23:00','ул. Мясницкая 102/3'),
 (6,'Deja vu','08:00','23:00','ул. Рыбникова 15');
INSERT INTO "Position" ("ID","title","salary","work schedule") VALUES (1,'Тренер',40000,'2/2'),
 (2,'Администратор',34500,'2/2'),
 (3,'Директор',57000,'5/2'),
 (4,'Уборщица',18000,'2/2'),
 (5,'Бухгалтер',38000,'4/3'),
 (6,'Охраник',20000,'5/2');
INSERT INTO "Product category" ("ID","name") VALUES (1,'Правильное питание'),
 (2,'Спорт одежда');
INSERT INTO "Lesson" ("ID","date and time","ID trainer","ID hall","ID subscription","ID program","ID gym") VALUES (1,'12:00 12.12.2021',1,3,4,1,4),
 (2,'15:30 20.03.2023',1,4,1,6,2),
 (3,'16:00 12.04.2023',1,2,2,3,1),
 (4,'09:00 20.12.2023',1,1,5,2,3),
 (5,'17:30 03.02.2023',1,5,3,4,6),
 (6,'18:35 12.01.2023',1,6,6,5,5);
COMMIT;
