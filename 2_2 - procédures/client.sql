CREATE OR REPLACE FUNCTION GetAllClients() RETURNS TABLE(
    id INT,
    first_name VARCHAR,
    last_name VARCHAR,
    email VARCHAR,
    street VARCHAR,
    postal_code VARCHAR,
    city VARCHAR,
    country VARCHAR,
    driving_license VARCHAR,
    birth_date DATE
) AS $$
BEGIN
    RETURN QUERY
    SELECT c.id, c.last_name, c.first_name, c.email,
           (address).street AS street,
           (address).postal_code AS postal_code,
           (address).city AS city,
           (address).country AS country,
           c.driving_license, c.birth_date
    FROM client c
    ORDER BY last_name;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetClientById(client_id INT) RETURNS TABLE(
    id INT,
    first_name VARCHAR,
    last_name VARCHAR,
    email VARCHAR,
    street VARCHAR,
    postal_code VARCHAR,
    city VARCHAR,
    country VARCHAR,
    driving_license VARCHAR,
    birth_date DATE
) AS $$
BEGIN
    RETURN QUERY
    SELECT c.id, c.last_name, c.first_name, c.email,
           (address).street AS street,
           (address).postal_code AS postal_code,
           (address).city AS city,
           (address).country AS country,
           c.driving_license, c.birth_date
    FROM client c
    WHERE c.id = client_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetClientByName(client_name VARCHAR) RETURNS TABLE(
    id INT,
    first_name VARCHAR,
    last_name VARCHAR,
    email VARCHAR,
    street VARCHAR,
    postal_code VARCHAR,
    city VARCHAR,
    country VARCHAR,
    driving_license VARCHAR,
    birth_date DATE
) AS $$
BEGIN
    RETURN QUERY
    SELECT c.id, c.last_name, c.first_name, c.email,
           (address).street AS street,
           (address).postal_code AS postal_code,
           (address).city AS city,
           (address).country AS country,
           c.driving_license, c.birth_date
    FROM client c
    WHERE c.last_name = client_name;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION GetClientByEmail(client_email VARCHAR) RETURNS TABLE(
    id INT,
    first_name VARCHAR,
    last_name VARCHAR,
    email VARCHAR,
    street VARCHAR,
    postal_code VARCHAR,
    city VARCHAR,
    country VARCHAR,
    driving_license VARCHAR,
    birth_date DATE
) AS $$
BEGIN
    RETURN QUERY
    SELECT c.id, c.last_name, c.first_name, c.email,
           (address).street AS street,
           (address).postal_code AS postal_code,
           (address).city AS city,
           (address).country AS country,
           c.driving_license, c.birth_date
    FROM client c
    WHERE c.email = client_email;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION createclient(
    last_name VARCHAR,
    first_name VARCHAR,
    email VARCHAR,
    street VARCHAR,
    postal_code VARCHAR,
    city VARCHAR,
    country VARCHAR,
    driving_license VARCHAR,
    birth_date TIMESTAMP
)
RETURNS INT AS $$
DECLARE
    new_id INT;
BEGIN
    INSERT INTO CLIENT(
        last_name,
        first_name,
        email,
        address,
        driving_license,
        birth_date
    )
    VALUES(
        $1,
        $2,
        $3,
        ROW(street, postal_code, city, country),
        $8,
        $9::DATE
    )
    RETURNING id INTO new_id;

    RETURN new_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION DeleteClient(client_id INT)
RETURNS BOOLEAN AS $$
BEGIN
    DELETE FROM client
    WHERE id = client_id;

    RETURN FOUND;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION UpdateClient(
    client_id INT,
    last_name VARCHAR,
    first_name VARCHAR,
    email VARCHAR,
    street VARCHAR,
    postal_code VARCHAR,
    city VARCHAR,
    country VARCHAR,
    driving_license VARCHAR,
    birth_date TIMESTAMP
)
RETURNS BOOLEAN AS $$
BEGIN
    UPDATE client
    SET
        last_name = $2,
        first_name = $3,
        email = $4,
        address = ROW($5, $6, $7, $8),
        driving_license = $9,
        birth_date = $10::DATE
    WHERE id = client_id;

    RETURN FOUND;
END;
$$ LANGUAGE plpgsql;