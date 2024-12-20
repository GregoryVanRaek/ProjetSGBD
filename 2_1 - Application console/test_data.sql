
INSERT INTO category (name, daily_rate)
VALUES
('Economy', 20.00),
('Compact', 30.00),
('Luxury', 50.00),
('4x4'n 80.00);

INSERT INTO model (name, brand, seat_number, category_id)
VALUES
('Fiat 500', 'Fiat', 4, 1),
('Toyota Corolla', 'Toyota', 5, 2),
('BMW X5', 'BMW', 5, 3);

INSERT INTO parking (code)
VALUES
('A01'),
('B10'),
('C15'),
('D20'),
('F05');

INSERT INTO car (license_plate, color, parking_id, model_id)
VALUES
('AB123CD', 'Red', 1, 1),
('XY456EF', 'Blue', 2, 2),
('ZK789GH', 'Black', 3, 3);

INSERT INTO client (last_name, first_name, email, address, driving_license, birth_date)
VALUES
('Doe', 'John', 'john.doe@example.com', ('123 Main St', '12345', 'City', 'Country'), 'D1234567', '1990-01-01'),
('Smith', 'Jane', 'jane.smith@example.com', ('456 Oak St', '67890', 'Town', 'Country'), 'S7654321', '1985-06-15');

INSERT INTO rental (car_id, client_id, start_date, duration, amount, status)
VALUES
(1, 1, '2024-12-25', 5, 100.00, 'reserved'),
(2, 2, '2024-12-20', 3, 90.00, 'rent');
