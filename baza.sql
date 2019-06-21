mysql> create database baza;
Query OK, 1 row affected (0.00 sec)

mysql> use baza;

mysql> create table cars(id int not null auto_increment primary key,manufacturer char(30) not null,model char(50),capacity char(3));
Query OK, 0 rows affected (0.13 sec)

mysql> show tables;
+----------------+
| Tables_in_baza |
+----------------+
| cars           |
+----------------+
1 row in set (0.00 sec)

mysql> desc cars;
+--------------+----------+------+-----+---------+----------------+
| Field        | Type     | Null | Key | Default | Extra          |
+--------------+----------+------+-----+---------+----------------+
| id           | int(11)  | NO   | PRI | NULL    | auto_increment |
| manufacturer | char(30) | NO   |     | NULL    |                |
| model        | char(50) | YES  |     | NULL    |                |
| capacity     | char(3)  | YES  |     | NULL    |                |
+--------------+----------+------+-----+---------+----------------+
4 rows in set (0.01 sec)

mysql> notee;
