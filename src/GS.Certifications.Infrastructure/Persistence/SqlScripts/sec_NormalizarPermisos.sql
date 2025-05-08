-- =========================================
-- Author:		Edgard Capurisse
-- Create date: 06/03/2025
-- Description:	Normaliza todos los permisos.
-- =============================================

--/****** Object:  StoredProcedure [dbo].[NormalizarPermisos]    Script Date: 26/3/2025 16:00:29 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[NormalizarPermisos] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   
EXEC AsignarTodosPermisosARole 1,0


IF NOT EXISTS (SELECT TOP 1 r.Id FROM sec_Roles AS r WHERE r.InternalCode = 'Supplier_Admin' AND r.IsDeleted = 0)
BEGIN
INSERT INTO [dbo].[sec_Roles]
			([InternalCode]
			,[Name]
			,[Description]
			,[GroupOwnerId]
			,[SystemUse]
			,[DomainFIdm]
			,[IsDeleted]
			,[Created]
			,[CreatedBy])
		VALUES
			('Supplier_Admin'
			,'SupplierAdmin'
			,'Administradores del Portal de Proveedores'
			,1000
			,1
			,1001
			,0
			,GETDATE()
			,'Sp_seedig')
END

IF NOT EXISTS (SELECT TOP 1 r.Id FROM sec_Roles AS r WHERE r.InternalCode = 'Supplier_User' AND r.IsDeleted = 0)
BEGIN
INSERT INTO [dbo].[sec_Roles]
			([InternalCode]
			,[Name]
			,[Description]
			,[GroupOwnerId]
			,[SystemUse]
			,[DomainFIdm]
			,[IsDeleted]
			,[Created]
			,[CreatedBy])
		VALUES
			('Supplier_User'
			,'SupplierUser'
			,'Usuarios del Portal de Proveedores'
			,1000
			,1
			,1001
			,0
			,GETDATE()
			,'Sp_seedig')
END

INSERT INTO sec_RolesGrants (RoleId, GrantId, IsDeleted, Created, CreatedBy)
SELECT 
	r.Id AS RoleId,
	g.Id AS GrantId,
	0 AS IsDeleted,
	GETDATE() AS Created,
	'Sp_seedig' AS CreatedBy
FROM 
	sec_Grants AS g
JOIN sec_Roles AS r ON
	r.InternalCode IN ('Supplier_User','Supplier_Admin')
	AND r.IsDeleted = 0
WHERE 
	g.Id NOT IN (SELECT rg.GrantId FROM sec_RolesGrants AS rg WHERE rg.RoleId = r.Id AND RG.IsDeleted = 0)
	AND g.DomainFIdm = 1001
	AND g.IsDeleted = 0



INSERT INTO sec_RolesOptions (RoleId, OptionId, IsDeleted, Created, CreatedBy)
SELECT 
	r.Id AS RoleId,
	o.Id AS OptionId,
	0 AS IsDeleted,
	GETDATE() AS Created,
	'Sp_seedig' AS CreatedBy
FROM 
	sec_Options AS o
JOIN sec_Roles AS r ON
	r.InternalCode IN ('Supplier_User','Supplier_Admin')
	AND r.IsDeleted = 0
WHERE 
	o.Id NOT IN (SELECT OptionId FROM sec_RolesOptions ro WHERE ro.RoleId = r.Id AND ro.IsDeleted = 0)
	AND o.DomainFIdm = 1001
	AND o.IsDeleted = 0




IF NOT EXISTS (SELECT TOP 1 g.Id FROM sec_Groups AS g WHERE g.InternalCode = 'AdminSupplier' AND g.IsDeleted = 0)
BEGIN
INSERT INTO [dbo].[sec_Groups]
           ([InternalCode]
           ,[Name]
           ,[Description]
           ,[GroupOwnerId]
           ,[SystemUse]
           ,[DomainFIdm]
           ,[IsDeleted]
           ,[Created]
           ,[CreatedBy])
     VALUES
           ('AdminSupplier'
           ,'AdminSupplier'
           ,'AdminSupplier'
           ,1
           ,1
           ,1001
           ,0
           ,GETDATE()
           ,'Sp_seedig')
END


IF NOT EXISTS (SELECT TOP 1 g.Id FROM sec_Groups AS g WHERE g.InternalCode = 'Supplier' AND g.IsDeleted = 0)
BEGIN
INSERT INTO [dbo].[sec_Groups]
           ([InternalCode]
           ,[Name]
           ,[Description]
           ,[GroupOwnerId]
           ,[SystemUse]
           ,[DomainFIdm]
           ,[IsDeleted]
           ,[Created]
           ,[CreatedBy])
     VALUES
           ('Supplier'
           ,'Supplier'
           ,'Supplier'
           ,1
           ,1
           ,1001
           ,0
           ,GETDATE()
           ,'Sp_seedig')
END


INSERT INTO [dbo].[sec_GroupsRoles]
           ([GroupId]
           ,[RoleId]
           ,[IsDeleted]
           ,[Created]
           ,[CreatedBy])
     SELECT
           g.Id AS GroupId
           ,r.Id AS RoleID
           ,0 AS IsDeleted
           ,GETDATE() AS Created
           ,'Sp_seedig' AS CreatedBy
	FROM sec_Groups AS g
	JOIN sec_Roles AS r ON
		r.InternalCode = 'Supplier_Admin'
		AND r.IsDeleted = 0
		AND NOT EXISTS (SELECT TOP 1 gr.Id FROM sec_GroupsRoles AS gr WHERE gr.GroupId = g.Id AND gr.RoleId = r.Id AND gr.IsDeleted = 0)
	WHERE
	g.InternalCode = 'AdminSupplier'
	AND g.IsDeleted = 0


INSERT INTO [dbo].[sec_GroupsRoles]
           ([GroupId]
           ,[RoleId]
           ,[IsDeleted]
           ,[Created]
           ,[CreatedBy])
     SELECT
           g.Id AS GroupId
           ,r.Id AS RoleID
           ,0 AS IsDeleted
           ,GETDATE() AS Created
           ,'Sp_seedig' AS CreatedBy
	FROM sec_Groups AS g
	JOIN sec_Roles AS r ON
		r.InternalCode = 'Supplier_User'
		AND r.IsDeleted = 0
		AND NOT EXISTS (SELECT TOP 1 gr.Id FROM sec_GroupsRoles AS gr WHERE gr.GroupId = g.Id AND gr.RoleId = r.Id AND gr.IsDeleted = 0)
	WHERE
	g.InternalCode = 'Supplier'
	AND g.IsDeleted = 0




INSERT INTO [dbo].[sec_CompaniesUsersGroups]
           ([CompanyId]
           ,[UserId]
           ,[GroupId]
           ,[IsDeleted]
           ,[Created]
           ,[CreatedBy])
     SELECT
           c.Id
           ,u.Id
           ,g.Id
           ,0
           ,GETDATE()
           ,'Sp_seedig'
	FROM sec_Users AS u
	JOIN sec_Groups AS g ON
		g.Id = 1
		AND g.IsDeleted = 0
	JOIN cny_Company AS c ON
		c.IsDeleted = 0
		AND NOT EXISTS (SELECT TOP 1 cug.Id FROM sec_CompaniesUsersGroups AS cug WHERE cug.UserId = u.Id AND cug.CompanyId = c.Id AND cug.GroupId = g.Id AND cug.IsDeleted = 0)
	WHERE
		u.Id = 1
		AND u.IsDeleted = 0




END
--GO





