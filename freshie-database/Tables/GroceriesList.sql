CREATE TABLE GroceriesList (
product_id INT,
user_id INT,
FOREIGN KEY (product_id) REFERENCES Products(product_id),
FOREIGN KEY (user_id) REFERENCES Users(user_id)
);