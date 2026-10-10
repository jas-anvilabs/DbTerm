namespace DbTerm.Infrastructure;

public static class InspectColumnQuery
{
    public static readonly string SingleColumn =
        """
        SELECT 
          a.attname AS column_name,
          pg_catalog.format_type(a.atttypid, a.atttypmod) AS data_type,
          a.attnotnull AS is_not_null
        FROM 
          pg_catalog.pg_attribute a
        JOIN 
          pg_catalog.pg_class c ON a.attrelid = c.oid
        JOIN 
          pg_catalog.pg_namespace n ON c.relnamespace = n.oid
        WHERE 
          n.nspname = @schema
          AND c.relname = @table
          AND a.attname = @column
          AND a.attnum > 0 
          AND NOT a.attisdropped
        ORDER BY 
          a.attnum;
        """;

    public static readonly string AllColumns =
        """
         SELECT 
           a.attname AS column_name,
           pg_catalog.format_type(a.atttypid, a.atttypmod) AS data_type,
           a.attnotnull AS is_not_null
         FROM 
           pg_catalog.pg_attribute a
         JOIN 
           pg_catalog.pg_class c ON a.attrelid = c.oid
         JOIN 
           pg_catalog.pg_namespace n ON c.relnamespace = n.oid
         WHERE 
           n.nspname = @schema
           AND c.relname = @table
           AND a.attnum > 0 
           AND NOT a.attisdropped
         ORDER BY 
           a.attnum;
         """;
}