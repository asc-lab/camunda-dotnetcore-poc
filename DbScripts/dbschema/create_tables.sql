CREATE TABLE public."Superpowers"
(
  "Id" uuid not null,
  "Code" character varying(50),
  "Name" character varying(150),
  CONSTRAINT Superpower_pk PRIMARY KEY ("Id")
);

CREATE TABLE public."Customers"
(
  "Id" uuid not null,
  "Code" character varying(50),
  "Name" character varying(150),
  CONSTRAINT Customer_pk PRIMARY KEY ("Id")
);  


CREATE TABLE public."Heroes"
(
  "Id" uuid not null,
  "Name" character varying(150),
  "DailyRate" numeric(19,2),
  CONSTRAINT Heroes_pk PRIMARY KEY ("Id")
);

CREATE TABLE public."HeroPower"
(
  "Id" uuid not null,
  "HeroId" uuid,
  "SuperpowerId" uuid,
  CONSTRAINT HeroPower_pk PRIMARY KEY ("Id"),
  CONSTRAINT HeroPower_Hero_fk FOREIGN KEY ("HeroId") REFERENCES "Heroes"("Id"),
  CONSTRAINT HeroPower_Superpower_fk FOREIGN KEY ("SuperpowerId") REFERENCES "Superpowers"("Id")
);

CREATE TABLE public."HeroAssignment"
(
  "Id" uuid not null,
  "HeroId" uuid,
  "CustomerId" uuid,
  "Period_From" date,
  "Period_To" date,
  "Status" character varying(50),
  CONSTRAINT HeroAssignment_pk PRIMARY KEY ("Id"),
  CONSTRAINT HeroAssignment_Hero_fk FOREIGN KEY ("HeroId") REFERENCES "Heroes"("Id"),
  CONSTRAINT HeroAssignment_Customer_fk FOREIGN KEY ("CustomerId") REFERENCES "Customers"("Id")
);

CREATE TABLE public."Orders"
(
  "Id" uuid not null,
  "SuperpowerId" uuid,
  "Period_From" date,
  "Period_To" date,
  "CustomerId" uuid,
  "Status" character varying(50),
  "ProcessInstanceId" character varying(50),
  "OfferId" uuid,  
  CONSTRAINT Orders_pk PRIMARY KEY ("Id"),
  CONSTRAINT Orders_Superpower_fk FOREIGN KEY ("SuperpowerId") REFERENCES "Superpowers"("Id"),
  CONSTRAINT Orders_Customer_fk FOREIGN KEY ("CustomerId") REFERENCES "Customers"("Id")
);

CREATE TABLE public."Offers"
(
  "Id" uuid not null,
  "OrderId" uuid,
  "AssignedHeroId" uuid,
  "Status" character varying(50),
  CONSTRAINT Offers_pk PRIMARY KEY ("Id"),
  CONSTRAINT Offers_Hero_fk FOREIGN KEY ("AssignedHeroId") REFERENCES "Heroes"("Id"),
  CONSTRAINT Offers_Order_fk FOREIGN KEY ("OrderId") REFERENCES "Orders"("Id")
);

CREATE TABLE public."Invoices"
(
  "Id" uuid not null,
  "OrderId" uuid,
  "CustomerId" uuid,
  "Title" character varying(250),
  "Status" character varying(50),
  "Amount" numeric(19,2),
  CONSTRAINT Invoices_pk PRIMARY KEY ("Id"),
  CONSTRAINT Invoices_Order_fk FOREIGN KEY ("OrderId") REFERENCES "Orders"("Id"),
  CONSTRAINT Invoices_Customer_fk FOREIGN KEY ("CustomerId") REFERENCES "Customers"("Id")
);

CREATE TABLE public."Notifications"
(
  "Id" uuid not null,
  "Text" character varying(500),
  "TargetGroup" character varying(150),
  "TargetUser" character varying(150),
  "IsRead" boolean,
  CONSTRAINT Notifications_pk PRIMARY KEY ("Id")
);