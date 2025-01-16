create function getallcars(only_available boolean)
    returns TABLE(id integer, license_plate character varying, color character varying, parking_code character varying, model_name character varying)
    language plpgsql
as
$$
BEGIN
    RETURN QUERY
    SELECT c.id, c.license_plate, c.color, p.code, m.name
    FROM car c
    LEFT JOIN parking p ON c.parking_id = p.id
    LEFT JOIN model m ON c.model_id = m.id
    WHERE $1 IS FALSE OR c.parking_id IS NOT NULL;
END;
$$;

alter function getallcars(boolean) owner to postgres;

