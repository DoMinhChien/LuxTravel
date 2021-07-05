

create table #tmp(ImagePath nvarchar(500),ImgLength int, ImageId int)
insert into #tmp
select ImagePath,DATALENGTH(ImagePath),ImageId from ImageStore

select * from #tmp
update img
set ImagePath = tmp.ImagePath
from ImageStore img
join #tmp tmp on img.ImageId = tmp.ImageId


update #tmp
set ImagePath =(select SUBSTRING(tmp.ImagePath,35, tmp.ImgLength -37))
from #tmp tmp

drop table #tmp


