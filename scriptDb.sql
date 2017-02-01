#------------------------------------------------------------
#        Script MySQL.
#------------------------------------------------------------


#------------------------------------------------------------
# Table: t_educanetFile
#------------------------------------------------------------

CREATE TABLE t_educanetFile(
        idFile            int (11) Auto_increment  NOT NULL ,
        eduName           Varchar (25) ,
        eduSize           Varchar (25) ,
        eduLastModified   Date ,
        eduDirectory      Varchar (25) ,
        idDateToActualise Int ,
        PRIMARY KEY (idFile )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: t_etmlFile
#------------------------------------------------------------

CREATE TABLE t_etmlFile(
        idFile            int (11) Auto_increment  NOT NULL ,
        etmlName          Varchar (25) ,
        etmlSize          Varchar (25) ,
        etmlLastModified  Date ,
        etmlDirectory     Varchar (25) ,
        idDateToActualise Int ,
        PRIMARY KEY (idFile )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: t_tempFile
#------------------------------------------------------------

CREATE TABLE t_tempFile(
        idFile            int (11) Auto_increment  NOT NULL ,
        tempName          Varchar (25) ,
        tempSize          Varchar (25) ,
        tempLastModified  Date ,
        tempDirectory     Varchar (25) ,
        idDateToActualise Int ,
        PRIMARY KEY (idFile )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: t_dateToActualise
#------------------------------------------------------------

CREATE TABLE t_dateToActualise(
        idDateToActualise int (11) Auto_increment  NOT NULL ,
        dtaMode           Int ,
        dateToActualise   Date ,
        PRIMARY KEY (idDateToActualise )
)ENGINE=InnoDB;

ALTER TABLE t_educanetFile ADD CONSTRAINT FK_t_educanetFile_idDateToActualise FOREIGN KEY (idDateToActualise) REFERENCES t_dateToActualise(idDateToActualise);
ALTER TABLE t_etmlFile ADD CONSTRAINT FK_t_etmlFile_idDateToActualise FOREIGN KEY (idDateToActualise) REFERENCES t_dateToActualise(idDateToActualise);
ALTER TABLE t_tempFile ADD CONSTRAINT FK_t_tempFile_idDateToActualise FOREIGN KEY (idDateToActualise) REFERENCES t_dateToActualise(idDateToActualise);
