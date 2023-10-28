CREATE TABLE Products (
product_id INT PRIMARY KEY,
product_name VARCHAR(100) NOT NULL,
icon VARCHAR(100),
category_id INT,
shelf_id INT,
FOREIGN KEY (category_id) REFERENCES Categories(category_id),
FOREIGN KEY (shelf_id) REFERENCES Shelves(shelf_id)
);
