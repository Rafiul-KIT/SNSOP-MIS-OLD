

CREATE PROCEDURE [dbo].[sp_InsUpdIPUserCreation] (@intUserid varchar(10),
@Ip varchar(10),
@Name varchar(100),
@Email varchar(150),
@Role varchar(100),
@Status varchar(10),
@userid varchar(40),
@password varchar(50),
--@password varchar(40),
@Created_by varchar(10))
AS
BEGIN
  IF (@intUserid <> '')
  BEGIN
    IF EXISTS (SELECT
        User_id
      FROM tbl_Users
      WHERE User_id = @userid and intUserid not like @intUserid)
    BEGIN
      SELECT
        1
    END
    ELSE
    BEGIN
    UPDATE tbl_Users
    SET User_Code = @Ip,
        User_name = @Name,
        Status = @Status,
        Email = @Email,
        Role = @Role,
        User_id = @userid,
        Password = @password,
        Modified_by = @Created_by,
        Modified_dt = GETDATE()

    WHERE intUserid = @intUserid

    SELECT
      100
    RETURN 100 END
  END
  ELSE
  BEGIN
    IF EXISTS (SELECT
        User_id
      FROM tbl_Users
      WHERE User_id = @userid)
    BEGIN
      SELECT
        1
    END
    ELSE
    BEGIN
      INSERT INTO tbl_Users (User_name, User_id, Password, Status, Role, Email, User_code, Created_by, Created_dt,User_level,User_type)
        VALUES (@Name, @userid, @password, @Status, @Role, @Email, @Ip, @Created_by, GETDATE(),'3','IP')
      SELECT
        @@identity
      RETURN @@identity
    END
  END
END
