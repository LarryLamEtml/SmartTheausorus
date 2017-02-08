DROP TABLE IF EXISTS t_educanetFile;
DROP TABLE IF EXISTS t_etmlFile;
DROP TABLE IF EXISTS t_tempFile;
DROP TABLE IF EXISTS t_dateToActualise;


CREATE TABLE t_educanetFile(
        idFile            int (11) NOT NULL PRIMARY KEY,
        eduName           Varchar (25) ,
        eduSize           Varchar (25) ,
        eduLastModified   Date ,
        eduDirectory      Varchar (25) ,
        idDateToActualise Int ,
		FOREIGN KEY(idDateToActualise) REFERENCES t_dateToActualise(idDateToActualise)
);

CREATE TABLE t_etmlFile(
        idFile            int (11) NOT NULL PRIMARY KEY,
        etmlName          Varchar (25) ,
        etmlSize          Varchar (25) ,
        etmlLastModified  Date ,
        etmlDirectory     Varchar (25) ,
        idDateToActualise Int ,
		FOREIGN KEY(idDateToActualise) REFERENCES t_dateToActualise(idDateToActualise)
);
CREATE TABLE t_tempFile(
        idFile            int (11)   NOT NULL PRIMARY KEY,
        tempName          Varchar (25) ,
        tempSize          Varchar (25) ,
        tempLastModified  Date ,
        tempDirectory     Varchar (25) ,
        idDateToActualise Int ,
		FOREIGN KEY(idDateToActualise) REFERENCES t_dateToActualise(idDateToActualise)
);

CREATE TABLE t_dateToActualise(
        idDateToActualise int (11) NOT NULL PRIMARY KEY,
        dtaMode           Int ,
        dateToActualise   Date 
);