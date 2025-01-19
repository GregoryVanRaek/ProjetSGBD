CREATE OR REPLACE FUNCTION getallrentals(with_completed_and_cancelled BOOLEAN)
RETURNS TABLE (
    id INT,
    car_id INT,
    client_id INT,
    start_date DATE,
    duration INT,
    amount NUMERIC,
    status VARCHAR,
    license_plate VARCHAR,
    client_name VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT r.id, r.car_id, r.client_id, r.start_date, r.duration, r.amount, r.status,
           c.license_plate, cl.last_name AS client_name
    FROM rental r
    LEFT JOIN car c ON r.car_id = c.id
    LEFT JOIN client cl ON r.client_id = cl.id
    WHERE with_completed_and_cancelled OR r.status IN ('rent', 'reserved')
    ORDER BY r.start_date, r.status;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION getrentalbyid(rental_id INT)
RETURNS TABLE (
    id INT,
    car_id INT,
    client_id INT,
    start_date DATE,
    duration INT,
    amount NUMERIC,
    status VARCHAR,
    license_plate VARCHAR,
    client_name VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT r.id, r.car_id, r.client_id, r.start_date, r.duration, r.amount, r.status,
           c.license_plate, cl.last_name AS client_name
    FROM rental r
    LEFT JOIN car c ON r.car_id = c.id
    LEFT JOIN client cl ON r.client_id = cl.id
    WHERE r.id = rental_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION createrental(
    car_id INT,
    client_id INT,
    start_date TIMESTAMP,
    duration INT,
    amount NUMERIC,
    status VARCHAR
)
RETURNS INT AS $$
DECLARE
    new_id INT;
BEGIN
    INSERT INTO rental (car_id, client_id, start_date, duration, amount, status)
    VALUES ($1, $2, $3, $4, $5, $6)
    RETURNING id INTO new_id;

    RETURN new_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION updaterental(
    rental_id INT,
    car_id INT,
    client_id INT,
    start_date TIMESTAMP,
    duration INT,
    amount NUMERIC,
    status VARCHAR
)
RETURNS VOID AS $$
BEGIN
    UPDATE rental
    SET car_id = $2, client_id = $3, start_date = $4, duration = $5, amount = $6, status = $7
    WHERE id = $1;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION deleterental(rental_id INT)
RETURNS BOOLEAN AS $$
BEGIN
    DELETE FROM rental
    WHERE id = rental_id;

    RETURN FOUND;
END;
$$ LANGUAGE plpgsql;