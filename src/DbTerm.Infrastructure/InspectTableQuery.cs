namespace DbTerm.Infrastructure;

public static class InspectTableQuery
{
    public static readonly string SingleTable =
        """
         SELECT 
             n.nspname AS schema_name,
             c.relname AS table_name,
             pg_catalog.pg_get_userbyid(c.relowner) AS table_owner,
             CASE c.relkind
                 WHEN 'r' THEN 'normal table'
                 WHEN 'v' THEN 'view'
                 WHEN 'm' THEN 'materialized view'
                 WHEN 'i' THEN 'index'
                 WHEN 'S' THEN 'sequence'
                 WHEN 't' THEN 'TOAST table'
                 WHEN 'f' THEN 'foreign table'
                 WHEN 'p' THEN 'partitioned table'
                 ELSE 'other'
             END AS table_type,
             c.reltuples AS estimated_row_count,
             pg_catalog.pg_size_pretty(pg_catalog.pg_total_relation_size(c.oid)) AS total_size_including_indexes
         FROM 
             pg_catalog.pg_class c
         JOIN 
             pg_catalog.pg_namespace n ON n.oid = c.relnamespace
         WHERE 
             n.nspname = @schema      -- Your schema (e.g., 'public')
             AND c.relname = @table   -- Your table name
             -- Optional: Remove the line below if you also want to look up views/sequences
             AND c.relkind IN ('r', 'p');
         """;
}
