CREATE TABLE FridgeItems (
fridge_item_id INT IDENTITY(1,1) PRIMARY KEY,
product_id INT,
user_id INT,
expiration_date DATE,
FOREIGN KEY (product_id) REFERENCES Products(product_id),
FOREIGN KEY (user_id) REFERENCES Users(user_id)
);