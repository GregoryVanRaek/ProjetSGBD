CREATE OR REPLACE FUNCTION getallcars(only_available BOOLEAN)
    RETURNS TABLE (
        id INT,
        license_plate VARCHAR,
        color VARCHAR,
        parking_code CHAR(3),
        model_name VARCHAR
    ) AS $$
BEGIN
    RETURN QUERY
        SELECT
            c.id,
            c.license_plate::VARCHAR,
            c.color::VARCHAR,
            p.code::CHAR(3),
            m.name::VARCHAR as model_name
        FROM car c
        LEFT JOIN parking p ON c.parking_id = p.id
        LEFT JOIN model m ON c.model_id = m.id
        WHERE only_available IS FALSE OR c.parking_id IS NOT NULL;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION getcarbyid(car_id INT)
RETURNS TABLE (
    id INT,
    license_plate VARCHAR,
    color VARCHAR,
    parking_code CHAR(3),
    model_name VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT
            c.id,
            c.license_plate::VARCHAR,
            c.color::VARCHAR,
            p.code::CHAR(3),
            m.name::VARCHAR as model_name
        FROM car c
    LEFT JOIN parking p ON c.parking_id = p.id
    LEFT JOIN model m ON c.model_id = m.id
    WHERE c.id = car_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION createcar(license_plate VARCHAR, color VARCHAR, parking_id INT, model_id INT)
RETURNS INT AS $$
DECLARE
    new_car_id INT;
BEGIN
    INSERT INTO car (license_plate, color, parking_id, model_id)
    VALUES ($1, $2, $3, $4)
    RETURNING id INTO new_car_id;

    RETURN new_car_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION updateparking(car_id INT, parking_id INT)
RETURNS VOID AS $$
BEGIN
    UPDATE car
    SET parking_id = $2
    WHERE id = $1;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION deletecar(car_id INT)
RETURNS VOID AS $$
BEGIN
    DELETE FROM car
    WHERE id = $1;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION getcaramount(car_id INT)
RETURNS NUMERIC AS $$
DECLARE
    daily_rate NUMERIC;
BEGIN
    SELECT cat.daily_rate
    INTO daily_rate
    FROM car c
    JOIN model m ON c.model_id = m.id
    JOIN category cat ON m.category_id = cat.id
    WHERE c.id = $1;

    RETURN daily_rate;
END;
$$ LANGUAGE plpgsql;
