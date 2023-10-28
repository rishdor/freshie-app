CREATE TABLE FridgeItems (
product_id INT,
user_id INT,
expiration_date DATE,
FOREIGN KEY (product_id) REFERENCES Products(product_id),
FOREIGN KEY (user_id) REFERENCES Users(user_id)
);