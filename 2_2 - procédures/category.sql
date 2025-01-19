CREATE OR REPLACE FUNCTION getallcategories()
RETURNS TABLE (
    id INT,
    name VARCHAR,
    daily_rate DECIMAL
) AS $$
BEGIN
    RETURN QUERY
    SELECT c.id, c.name, c.daily_rate
    FROM category c
    ORDER BY c.id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION getcategorybyid(category_id INT)
RETURNS TABLE (
    id INT,
    name VARCHAR,
    daily_rate DECIMAL
) AS $$
BEGIN
    RETURN QUERY
    SELECT c.id, c.name, c.daily_rate
    FROM category c
    WHERE c.id = $1;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION createcategory(
    category_name VARCHAR,
    category_daily_rate DECIMAL
)
RETURNS INT AS $$
DECLARE
    new_id INT;
BEGIN
    INSERT INTO category(name, daily_rate)
    VALUES($1, $2)
    RETURNING id INTO new_id;

    RETURN new_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION updatecategory(
    category_id INT,
    category_name VARCHAR,
    category_daily_rate DECIMAL
)
RETURNS BOOLEAN AS $$
BEGIN
    UPDATE category
    SET name = $2,
        daily_rate = $3
    WHERE id = $1;

    RETURN FOUND;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION deletecategory(category_id INT)
RETURNS BOOLEAN AS $$
BEGIN
    DELETE FROM category WHERE id = $1;
    RETURN FOUND;
END;
$$ LANGUAGE plpgsql;