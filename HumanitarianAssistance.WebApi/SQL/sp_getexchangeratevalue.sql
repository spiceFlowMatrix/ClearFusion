CREATE
OR REPLACE FUNCTION public.get_exchangerate_value(
    amount double precision DEFAULT 0,
    from_currency integer DEFAULT 0,
    to_currency integer DEFAULT 0,
    transaction_date timestamp without time zone DEFAULT NULL :: timestamp without time zone,
    OUT _final_amount double precision
) RETURNS double precision LANGUAGE 'plpgsql' COST 100 VOLATILE AS $Body$ BEGIN --    SELECT INTO _col1  col1 FROM TABLE WHERE id = _param_id;
SELECT
    INTO _final_amount (amount * exr."Rate")
FROM
    "ExchangeRateDetail" AS exr
WHERE
    "IsDeleted" = false
    AND "FromCurrency" = from_currency
    AND "ToCurrency" = to_currency
    AND ("Date" :: DATE) = (transaction_date :: DATE)
ORDER BY
    exr."Date" DESC
limit
    1;

END $Body$;