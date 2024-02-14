CREATE OR REPLACE FUNCTION Get_Total_Cost(text) RETURNS numeric
language sql
as $$
    WITH RECURSIVE subitem AS (
      SELECT 
        id,
        item_name,
        parent_item, 
        cost 
      FROM 
        item 
      WHERE 
        $1 = any(item_name)
      UNION 
      SELECT 
        i.id,
        i.item_name,
        i.parent_item, 
        i.cost 
      FROM 
        item i 
        INNER JOIN subitem s ON s.id = i.parent_item
    ) 
    SELECT SUM(cost) FROM subitem;
$$;
