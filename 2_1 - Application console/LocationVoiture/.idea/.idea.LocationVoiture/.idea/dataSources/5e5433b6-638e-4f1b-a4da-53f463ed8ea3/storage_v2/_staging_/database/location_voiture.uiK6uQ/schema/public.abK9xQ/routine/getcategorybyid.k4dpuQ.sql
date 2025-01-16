create function getcategorybyid(category_id integer)
    returns TABLE(id integer, name character varying, daily_rate numeric)
    language plpgsql
as
$$
BEGIN
    RETURN QUERY
    SELECT c.id, c.name, c.daily_rate
    FROM category c
    WHERE c.id = category_id;
END;
$$;

alter function getcategorybyid(integer) owner to postgres;

