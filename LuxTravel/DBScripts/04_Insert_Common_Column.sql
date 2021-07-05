-- IsDeleted
EXEC sp_MSforeachtable '
if not exists (select * from sys.columns 
               where object_id = object_id(''?'')
               and name = ''IsDeleted'') 
begin
    ALTER TABLE ? ADD IsDeleted bit NOT NULL DEFAULT (0);
end';
-- CreatedBy
EXEC sp_MSforeachtable '
if not exists (select * from sys.columns 
               where object_id = object_id(''?'')
               and name = ''CreatedBy'') 
begin
    ALTER TABLE ? ADD CreatedBy UNIQUEIDENTIFIER  NULL;
end';

-- ModifiedBy
EXEC sp_MSforeachtable '
if not exists (select * from sys.columns 
               where object_id = object_id(''?'')
               and name = ''ModifiedBy'') 
begin
    ALTER TABLE ? ADD ModifiedBy UNIQUEIDENTIFIER  NULL;
end';


-- CreatedOn
EXEC sp_MSforeachtable '
if not exists (select * from sys.columns 
               where object_id = object_id(''?'')
               and name = ''CreatedOn'') 
begin
    ALTER TABLE ? ADD CreatedOn DATETIME NOT NULL DEFAULT(Getdate());
end';

-- ModifiedOn
EXEC sp_MSforeachtable '
if not exists (select * from sys.columns 
               where object_id = object_id(''?'')
               and name = ''ModifiedOn'') 
begin
    ALTER TABLE ? ADD ModifiedOn DATETIME NULL;
end';
