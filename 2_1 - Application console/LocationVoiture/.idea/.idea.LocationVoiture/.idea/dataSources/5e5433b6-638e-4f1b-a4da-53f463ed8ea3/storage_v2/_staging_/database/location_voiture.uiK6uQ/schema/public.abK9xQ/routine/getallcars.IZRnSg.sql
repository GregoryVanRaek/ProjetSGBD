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

