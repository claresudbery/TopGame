delete from GoldenMasterSinglePasses
delete from VitalStatistics
delete from TopGameGraphicsPathTopGamePoints
delete from TopGameGraphicsPaths
delete from TopGamePointCollectionTopGamePoints
delete from TopGamePointCollections
delete from TopGamePointTopGameRegions
delete from TopGameRegions
delete from TopGameRectangles
delete from TopGamePoints

select count(*) from GoldenMasterSinglePasses
select count(*) from TopGameGraphicsPaths
select count(*) from TopGameGraphicsPathTopGamePoints
select count(*) from TopGamePointCollections
select count(*) from TopGamePointCollectionTopGamePoints
select count(*) from TopGamePoints
select count(*) from TopGameRectangles
select count(*) from TopGameRegions
select count(*) from TopGameRegionTopGamePoints
select count(*) from VitalStatistics

select * from GoldenMasterSinglePasses
select * from TopGameGraphicsPaths
select * from TopGameGraphicsPathTopGamePoints
select * from TopGamePointCollections
select * from TopGamePointCollectionTopGamePoints
select * from TopGamePoints
select * from TopGameRectangles
select * from TopGameRegions
select * from TopGameRegionTopGamePoints
select * from VitalStatistics