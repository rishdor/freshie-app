CREATE TABLE GroceriesHistory (
product_id INT,
user_id INT,
date DATE,
purchased BIT,
FOREIGN KEY (product_id) REFERENCES Products(product_id),
FOREIGN KEY (user_id) REFERENCES Users(user_id)

);