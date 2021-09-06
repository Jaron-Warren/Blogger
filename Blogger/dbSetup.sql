CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS blogs(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title varchar(20) COMMENT 'Blog Title',
  body varchar(255) COMMENT 'Blog body',
  imgUrl varchar(255) COMMENT 'Blog picture',
  published TINYINT COMMENT 'Has Blog been published?',
  creatorId varchar(255) COMMENT 'Creator Id'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS comments(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId varchar(255) COMMENT 'Creator Id',
  body varchar(240) NOT NULL COMMENT 'comment body',
  blog varchar(255) NOT NULL COMMENT 'blog comment belongs to'
) default charset utf8 COMMENT '';
INSERT INTO
  blogs (
    title,
    body,
    imgUrl,
    published,
    creatorId
  )
VALUES
  (
    "TEST2",
    "this is another test",
    "fakeimg123.com",
    0,
    "6133ec636ec5298eb206f1d4"
  );
DROP TABLE comments