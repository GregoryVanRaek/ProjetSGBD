CREATE OR REPLACE FUNCTION getallmodels()
RETURNS TABLE (
    id INT,
    name VARCHAR,
    brand VARCHAR,
    seat_number INT,
    category_id INT,
    category_name VARCHAR,
    daily_rate DECIMAL
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        m.id,
        m.name,
        m.brand,
        m.seat_number,
        m.category_id,
        c.name as category_name,
        c.daily_rate
    FROM model m
    LEFT JOIN category c ON m.category_id = c.id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION getmodelbyid(model_id INT)
RETURNS TABLE (
    id INT,
    name VARCHAR,
    brand VARCHAR,
    seat_number INT,
    category_id INT,
    category_name VARCHAR,
    daily_rate DECIMAL
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        m.id,
        m.name,
        m.brand,
        m.seat_number,
        m.category_id,
        c.name as category_name,
        c.daily_rate
    FROM model m
    LEFT JOIN category c ON m.category_id = c.id
    WHERE m.id = model_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION createmodel(
    model_name VARCHAR,
    model_brand VARCHAR,
    model_seat_number INT,
    model_category_id INT
)
RETURNS INT AS $$
DECLARE
    new_id INT;
BEGIN
    INSERT INTO model(name, brand, seat_number, category_id)
    VALUES(model_name, model_brand, model_seat_number, model_category_id)
    RETURNING id INTO new_id;

    RETURN new_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION deletemodel(model_id integer)
RETURNS BOOLEAN AS $$
BEGIN
    DELETE FROM model WHERE id = model_id;
    RETURN FOUND;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION updatemodel(
    model_id INT,
    model_name VARCHAR,
    model_brand VARCHAR,
    model_seat_number INT,
    model_category_id INT
)
RETURNS BOOLEAN AS $$
BEGIN
    UPDATE model
    SET
        name = model_name,
        brand = model_brand,
        seat_number = model_seat_number,
        category_id = model_category_id
    WHERE id = model_id;

    RETURN FOUND;
END;
$$ LANGUAGE plpgsql;

