-- This script is in charge of creating the DB model. It will run when the volume is created. 
-- If the process stops for any reason or the DB is created incorrectly, remove the container and 
-- volume. To remove the volume use the following command 'docker volume rm <name-volume>' (in this case the volume name is 'general_db_volume'), then
-- run 'docker build -t viwit-general .' , 'docker-compose build' and 'docker-compose up' again.

DROP TABLE IF EXISTS public."Route" CASCADE;
CREATE TABLE public."Route" ( "IdRoute" integer NOT NULL, "NameRoute" varchar(40) NOT NULL, "InitialBusStop" varchar(40) NOT NULL, "FinalBusStop" varchar(40) NOT NULL, "ApproximateDuration" varchar(40) NOT NULL, CONSTRAINT "Route_pk" PRIMARY KEY ("IdRoute") ); ALTER TABLE public."Route" OWNER TO postgres;

DROP INDEX IF EXISTS public."IdRoute_UNIQUE" CASCADE;
CREATE UNIQUE INDEX "IdRoute_UNIQUE"
ON public."Route" USING btree ( "IdRoute" );

DROP INDEX IF EXISTS public."NameRoute_UNIQUE" CASCADE;
CREATE UNIQUE INDEX "NameRoute_UNIQUE"
ON public."Route" USING btree ( "NameRoute" );

DROP TABLE IF EXISTS public."BusStop" CASCADE;
CREATE TABLE public."BusStop" ( "IdBusStop" integer NOT NULL, "NameOrAddressBusStop" varchar(50) NOT NULL, CONSTRAINT "BusStop_pk" PRIMARY KEY ("IdBusStop") ); ALTER TABLE public."BusStop" OWNER TO postgres;

DROP INDEX IF EXISTS public."IdBusStop_UNIQUE" CASCADE;
CREATE UNIQUE INDEX "IdBusStop_UNIQUE"
ON public."BusStop" USING btree ( "IdBusStop" );

DROP INDEX IF EXISTS public."NameOrAddressBusStop_UNIQUE" CASCADE;
CREATE UNIQUE INDEX "NameOrAddressBusStop_UNIQUE"
ON public."BusStop" USING btree ( "NameOrAddressBusStop" );

DROP TABLE IF EXISTS public."RouteStops" CASCADE;
CREATE TABLE public."RouteStops" ( "IdRoute_Route" integer NOT NULL, "IdBusStop_BusStop" integer NOT NULL, CONSTRAINT "RouteStops_pk" PRIMARY KEY ("IdRoute_Route","IdBusStop_BusStop") ); ALTER TABLE public."RouteStops" OWNER TO postgres; ALTER TABLE public."RouteStops"

DROP CONSTRAINT IF EXISTS "Route_fk" CASCADE; ALTER TABLE public."RouteStops" ADD CONSTRAINT "Route_fk" FOREIGN KEY ("IdRoute_Route") REFERENCES public."Route" ("IdRoute") MATCH FULL
ON DELETE CASCADE
ON UPDATE CASCADE; ALTER TABLE public."RouteStops"

DROP CONSTRAINT IF EXISTS "BusStop_fk" CASCADE; ALTER TABLE public."RouteStops" ADD CONSTRAINT "BusStop_fk" FOREIGN KEY ("IdBusStop_BusStop") REFERENCES public."BusStop" ("IdBusStop") MATCH FULL
ON DELETE CASCADE
ON UPDATE CASCADE;

DROP TABLE IF EXISTS public."Bus" CASCADE;
CREATE TABLE public."Bus" ( "LicensePlateBus" varchar(10) NOT NULL, "Model" varchar(10) NOT NULL, "SeatedPassengerCapacity" integer NOT NULL, "StandingPassengerCapacity" integer NOT NULL, CONSTRAINT "Bus_pk" PRIMARY KEY ("LicensePlateBus") ); ALTER TABLE public."Bus" OWNER TO postgres;

DROP INDEX IF EXISTS public."LicensePlateBus_UNIQUE" CASCADE;
CREATE UNIQUE INDEX "LicensePlateBus_UNIQUE"
ON public."Bus" USING btree ( "LicensePlateBus" );

DROP TABLE IF EXISTS public."Driver" CASCADE;
CREATE TABLE public."Driver" ( "DriversLicense" varchar(20) NOT NULL, "Name" varchar(60) NOT NULL, "DriverExperience" integer NOT NULL, "AverageDriverRating" varchar(30) NOT NULL, CONSTRAINT "Driver_pk" PRIMARY KEY ("DriversLicense") ); ALTER TABLE public."Driver" OWNER TO postgres;

DROP INDEX IF EXISTS public."DriversLicense_UNIQUE" CASCADE;
CREATE UNIQUE INDEX "DriversLicense_UNIQUE"
ON public."Driver" USING btree ( "DriversLicense" );

DROP TABLE IF EXISTS public."Trip" CASCADE;
CREATE TABLE public."Trip" ( "IdTrip" varchar(20) NOT NULL, "IdRoute_Route" integer NOT NULL, "LicensePlateBus_Bus" varchar(10) NOT NULL, "DriversLicense_Driver" varchar(20) NOT NULL, "IdBusStop_BusStop" integer, "CurrentTripOccupation" integer NOT NULL, "StartDate" varchar(20) NOT NULL, "TripStatus" varchar(10) NOT NULL, CONSTRAINT "Trip_pk" PRIMARY KEY ("IdTrip","IdRoute_Route","LicensePlateBus_Bus","DriversLicense_Driver") ); ALTER TABLE public."Trip" OWNER TO postgres;

DROP INDEX IF EXISTS public."IdTrip_UNIQUE" CASCADE;
CREATE UNIQUE INDEX "IdTrip_UNIQUE"
ON public."Trip" USING btree ( "IdTrip" ); ALTER TABLE public."Trip"

DROP CONSTRAINT IF EXISTS "Route_fk" CASCADE; ALTER TABLE public."Trip" ADD CONSTRAINT "Route_fk" FOREIGN KEY ("IdRoute_Route") REFERENCES public."Route" ("IdRoute") MATCH FULL
ON DELETE CASCADE
ON UPDATE CASCADE; ALTER TABLE public."Trip"

DROP CONSTRAINT IF EXISTS "Bus_fk" CASCADE; ALTER TABLE public."Trip" ADD CONSTRAINT "Bus_fk" FOREIGN KEY ("LicensePlateBus_Bus") REFERENCES public."Bus" ("LicensePlateBus") MATCH FULL
ON DELETE CASCADE
ON UPDATE CASCADE; ALTER TABLE public."Trip"

DROP CONSTRAINT IF EXISTS "Driver_fk" CASCADE; ALTER TABLE public."Trip" ADD CONSTRAINT "Driver_fk" FOREIGN KEY ("DriversLicense_Driver") REFERENCES public."Driver" ("DriversLicense") MATCH FULL
ON DELETE CASCADE
ON UPDATE CASCADE; ALTER TABLE public."Trip"

DROP CONSTRAINT IF EXISTS "BusStop_fk" CASCADE; ALTER TABLE public."Trip" ADD CONSTRAINT "BusStop_fk" FOREIGN KEY ("IdBusStop_BusStop") REFERENCES public."BusStop" ("IdBusStop") MATCH FULL
ON DELETE

SET NULL
ON UPDATE CASCADE; ALTER TABLE public."Trip"
