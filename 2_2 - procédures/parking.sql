CREATE OR REPLACE FUNCTION getparkingcode(parking_code INT)
RETURNS TABLE (
    id INT,
    code CHAR(3)
) AS $$
BEGIN
    RETURN QUERY
    SELECT p.id, p.code
    FROM parking p
    WHERE p.id = $1;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION getallparkingcodes(only_available BOOLEAN)
RETURNS TABLE (
    id INT,
    code CHAR(3)
) AS $$
BEGIN
    IF $1 THEN
        RETURN QUERY
        SELECT p.id, p.code
        FROM parking p
        WHERE p.id NOT IN (SELECT car.parking_id FROM car WHERE car.parking_id IS NOT NULL);
    ELSE
        RETURN QUERY
        SELECT p.id, p.code
        FROM parking p;
    END IF;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION assignfreeparkingplace(car_id INT)
RETURNS BOOLEAN AS $$
DECLARE
    free_parking_id INT;
BEGIN
    SELECT p.id
    INTO free_parking_id
    FROM parking p
    WHERE p.id NOT IN (SELECT c.parking_id FROM car c WHERE c.parking_id IS NOT NULL)
    LIMIT 1;

    IF free_parking_id IS NOT NULL THEN
        UPDATE car
        SET parking_id = free_parking_id
        WHERE id = $1;

        RETURN TRUE;
    END IF;

    RETURN FALSE;
END;
$$ LANGUAGE plpgsql;

