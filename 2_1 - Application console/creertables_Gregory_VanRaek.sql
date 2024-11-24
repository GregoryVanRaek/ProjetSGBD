CREATE TABLE category (
    id SERIAL NOT NULL,
    name VARCHAR(25) NOT NULL,
    daily_rate NUMERIC(5, 2) NOT NULL,
    CONSTRAINT pk_category PRIMARY KEY (id)
);

CREATE TABLE model (
    id SERIAL NOT NULL,
    name VARCHAR(50) NOT NULL,
    brand VARCHAR(50) NOT NULL,
    seat_number INT NOT NULL,
    category_id INT NOT NULL,
    CONSTRAINT pk_model PRIMARY KEY (id),
    CONSTRAINT fk_category FOREIGN KEY (category_id) REFERENCES category(id)
);

CREATE TABLE parking (
    id SERIAL NOT NULL,
    code CHAR(3) NOT NULL,
    CONSTRAINT pk_parking_spot PRIMARY KEY (id),
    CONSTRAINT uk_parking_spot UNIQUE (code),
    CONSTRAINT check_parking_code CHECK (code ~ '^[A-F](0[1-9]|1[0-9]|20)$')
);

CREATE TABLE car (
    id SERIAL NOT NULL,
    license_plate VARCHAR(20) NOT NULL,
    color VARCHAR(20) NOT NULL,
    parking_id INT, -- gérer via un trigger + procédure stockée qu'on n'attribue pas 2 voitures sur 1 même emplacement et asigner à null lorsqu'une voiture est louée
    model_id INT NOT NULL,
    CONSTRAINT pk_car PRIMARY KEY (id),
    CONSTRAINT fk_parking FOREIGN KEY (parking_id) REFERENCES parking(id),
    CONSTRAINT fk_model FOREIGN KEY (model_id) REFERENCES model(id),
    CONSTRAINT uk_license_plate UNIQUE (license_plate)
);

CREATE TYPE address AS (
    street VARCHAR(50),
    postal_code VARCHAR(10),
    city VARCHAR(30),
    country VARCHAR(30)
);

CREATE TABLE client (
    id SERIAL NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    address address NOT NULL,
    driving_license VARCHAR(50) NOT NULL,
    birth_date DATE NOT NULL,
    CONSTRAINT pk_client PRIMARY KEY (id),
    CONSTRAINT uk_email UNIQUE (email),
    CONSTRAINT uk_driving_license UNIQUE (driving_license),
    CONSTRAINT check_age CHECK (EXTRACT(YEAR FROM AGE(birth_date)) >= 21)
);

CREATE TABLE rental (
    id SERIAL NOT NULL,
    car_id INT NOT NULL,
    client_id INT NOT NULL,
    start_date DATE NOT NULL,
    duration INT NOT NULL,
    amount NUMERIC(10, 2) NOT NULL,
    status VARCHAR(10) NOT NULL,
    CONSTRAINT pk_rental PRIMARY KEY (id),
    CONSTRAINT fk_car FOREIGN KEY (car_id) REFERENCES car(id),
    CONSTRAINT fk_client FOREIGN KEY (client_id) REFERENCES client(id),
    CONSTRAINT check_status CHECK (status IN ('rent', 'completed', 'reserved', 'cancelled'))
);



