create table stock (
	id bigserial primary key,
	name varchar(255) not null check(length(name) > 2),
	creation_date date not null check(extract(year from creation_date) >= 2000 and extract(year from creation_date) <= extract(year from current_date)),
	days_valid_to smallint not null check(days_valid_to > 0),
	price decimal not null check(price > 0),
	measure varchar(63) not null
);

create table provider (
	id bigserial primary key,
	name varchar(255) not null check(length(name) > 2),
	address varchar(511) not null check(length(address) > 2),
	phone char(10) not null check(length(phone) = 10),
	contact_person varchar(255)
);

create table delivery (
	stock_id bigserial,
	provider_id bigserial,
	document_number bigint not null check(document_number > 0),
	delivery_date date not null,
	quantity bigint not null check(quantity > 0),
	primary key(stock_id, provider_id),
	foreign key(stock_id) references stock(id),
	foreign key(provider_id) references provider(id)
);

insert into stock (name, creation_date, days_valid_to, price, measure)
values
	('����', current_date - 3, 7, 4.5, '�����'),
	('����', current_date - 5, 10, 0.4, '����'),
	('����', current_date - 2, 5, 1.2, '���������'),
	('������', current_date - 2, 3, 6.2, '���������'),
	('����', current_date - 1, 20, 2.5, '�����');

insert into provider (name, address, phone, contact_person)
values 
	('������ ���', '����������� 12', '0892423124', '���� ������'),
	('��������� ���', '��������� 44', '0883442334', '������ ������'),
	('������� ���', '������������ 18', '0893452367', '����� ������'),
	('��� ���', '��������� 23', '0897005345', '��� ��������'),
	('�������� ���', '����� 11', '0893442576', '������ ��������');

insert into delivery (stock_id, provider_id, delivery_date, document_number, quantity)
values 
	(1, 2, current_date - 1, 103, 2),
	(1, 3, current_date - 2, 104, 3),
	(1, 4, current_date - 3, 105, 5),
	(1, 5, current_date - 4, 106, 22),
	(2, 1, current_date - 5, 107, 11),
	(2, 2, current_date - 6, 108, 23),
	(3, 1, current_date - 7, 109, 55),
	(4, 3, current_date - 8, 110, 32),
	(5, 5, current_date - 9, 111, 7),
	(5, 2, current_date - 10, 112, 6);