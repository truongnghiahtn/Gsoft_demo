create database KhaiBaoYTe
use KhaiBaoYTe

go
create table ChuDe(
	IDChuDe int primary key not null,
	TenChuDe nvarchar(max),
	MoTa nvarchar(max),
	NgayTao datetime,
	NguoiTao nvarchar(50),
	NgayUpdate datetime,
	NguoiUpdate nvarchar(50)
)
go
create table Template(
	IDTemplate int primary key not null,
	IDChuDe int,
	TenTemplate nvarchar(max),
	MoTa nvarchar(max),
	NgayTao datetime,
	NguoiTao nvarchar(50),
	NgayUpdate datetime,
	NguoiUpdate nvarchar(50),
	HienThi int default 5,
	TinhDiem bit,
	Random bit,
	TemplateEnable bit
)
go
create table CauHoi(
	IDCauHoi int primary key not null,
	IDTemplate int,
	TieuDe nvarchar(max),
	IDLoaiCauHoi int,
	CauHoiRequired bit not null,
	NgayTao datetime,
	NguoiTao nvarchar(50),
	NgayUpdate datetime,
	NguoiUpdate nvarchar(50),
	SoDiem int,
	CauHoiEnable bit
)
go
create table CauTraLoiDung(
	IDCauHoi int,
	CauTraLoi_Dung nvarchar(max),
	ID_CTL_Dung int identity(1,1)
)
go
create table Sub_CauHoi(
	IDSubCauHoi int primary key not null,
	IDCauHoi int,
	NoiDung nvarchar(max)
)
go
create table LoaiCauHoi(
	IDLoaiCauHoi int primary key not null,
	DangCauHoi nvarchar(50)
)
go
create table CauTraLoi(
	IDCauTraLoi int primary key not null,
	UserID int,
	HoTen nvarchar(50),
	MSNV varchar(10),
	Email varchar(100),
	IDChuDe int,
	IDTemplate int
)
go
create table CauTraLoi_ChiTiet (
	IDCauTraLoiChiTiet int primary key not null,
	IDCauTraLoi int,
	IDCauHoi int,
	CauTraLoi nvarchar(max)
)

go
create trigger tr1 on ChuDe after insert
as
begin
	update ChuDe set NgayTao = GETDATE() from ChuDe cd join inserted i on cd.IDChuDe = i.IDChuDe
end

go
create trigger tr2 on ChuDe after update
as
begin
	update ChuDe set NgayUpdate = GETDATE() from ChuDe cd join inserted i on cd.IDChuDe = i.IDChuDe
end

go
create trigger tr3 on Template after insert
as
begin
	update Template set NgayTao = GETDATE() from Template t join inserted i on t.IDTemplate = i.IDTemplate
end

go
create trigger tr4 on Template after update
as
begin
	update Template set NgayUpdate = GETDATE() from Template t join inserted i on t.IDTemplate = i.IDTemplate
end

go
create trigger tr5 on CauHoi after insert
as
begin
	update CauHoi set NgayTao = GETDATE() from CauHoi ch join inserted i on ch.IDCauHoi = i.IDCauHoi
end

go
create trigger tr6 on CauHoi after insert
as
begin
	update CauHoi set NgayUpdate = GETDATE() from CauHoi ch join inserted i on ch.IDCauHoi = i.IDCauHoi
end

alter table Template
add constraint fk_Template_ChuDe foreign key (IDChuDe) references ChuDe(IDChuDe)

alter table CauHoi
add constraint fk_CauHoi_Template foreign key (IDTemplate) references Template(IDTemplate)

alter table CauHoi
add constraint fk_CauHoi_LoaiCauHoi foreign key (IDLoaiCauHoi) references LoaiCauHoi(IDLoaiCauHoi)

alter table Sub_CauHoi
add constraint fk_Sub_CauHoi_CauHoi foreign key (IDCauHoi) references CauHoi(IDCauHoi)

alter table CauTraLoi
add constraint fk_CauTraLoi_ChuDe foreign key (IDChuDe) references ChuDe(IDChuDe)

alter table CauTraLoi
add constraint fk_CauTraLoi_Template foreign key (IDTemplate) references Template(IDTemplate)

alter table CauTraLoi_ChiTiet
add constraint fk_CauTraLoi_ChiTiet_CauTraLoi foreign key (IDCauTraLoi) references CauTraLoi(IDCauTraLoi)

alter table CauTraLoi_ChiTiet
add constraint fk_CauTraLoi_ChiTiet_CauHoi foreign key (IDCauHoi) references CauHoi(IDCauHoi)

------------------------------------------------------------------------------------------------------------------------------
-- Chủ đề
insert into ChuDe values (1, N'Y Tế', N'Thư viện các Template và bộ câu hỏi về lĩnh vực Y Tế', null, N'Xuân Hương', null, null)
insert into ChuDe values (2, N'Giáo Dục', N'Thư viện các Template và bộ câu hỏi về lĩnh vực Giáp Dục và Đào Tạo', null, N'Lý Sang', null, null)

-- Template
insert into Template values(1, 1, N'Tờ khai báo y tế tự nguyện công ty GSOFT và GOBRANDING',
 N'Bằng cách khai báo y tế trên ứng dụng NCOVI, mỗi chúng ta đã đóng góp phần công sức vào công cuộc phòng và chống đại
  dịch cúm Corona, giúp các cơ quan nhà nước, Bộ Y Tế có thể thống kê, kiểm soát tình hình và thực hiện các biện pháp 
  cách ly chính xác và nhanh chóng, trách lây lan.', null, N'Thanh Khiết', null, null, 5, 0, 0, 0)
insert into Template values(2, 2, N'Bài thi thử toán lớp 3', N'Bài thi thử giúp các em học sinh ôn tập, củng cố,
 rèn luyện kỹ năng giải các dạng Toán, chuẩn bị tốt cho kì thi học kì 1 lớp 3, đồng thời sẽ giúp các em tự học, tự nâng
  cao kiến thức môn Toán 3.', null, N'Thanh Khiết', null, null, 5, 1, 0, 1)

 --LoaiCauHoi
insert into LoaiCauHoi values(1, N'TextBox')
insert into LoaiCauHoi values(2, N'CheckBox')
insert into LoaiCauHoi values(3, N'RadioButton')

----CauHoi------
insert into CauHoi values(1, 1, N'Có tiếp xúc với trường hợp bệnh hoặc nghi ngờ mắc bệnh COVID 19', 3, 1, null, N'Thanh Khiết', null, null, 0, 0)
insert into CauHoi values(2, 1, N'Có đi từ khu vực có dịch bệnh COVID 19', 3, 1, null, N'Thanh Khiết', null, null, 0, 0)
insert into CauHoi values(3, 1, N'Có tiếp xúc với trường hợp đi từ khu có dịch bệnh COVID 19', 3, 1, null, N'Thanh Khiết', null, null, 0, 0)
insert into CauHoi values(4, 1, N'Trong vòng 3 ngày Anh/ Chị đã đi đến những khu vực vị trí nào trong quốc gia và ngoài quốc gia?', 1, 0, null, N'Thanh Khiết', null, null, 0, 0)
insert into CauHoi values(5, 1, N'Trong vòng 14 ngày Anh/ Chị có dấu hiệu nào sau đây không?', 2, 0, null, N'Thanh Khiết', null, null, 0, 0)
insert into CauHoi values(6, 1, N'Trong vòng 14 ngày Anh/ Chị có tiếp xúc với:', 2, 0, null, N'Thanh Khiết', null, null, 0, 0)
insert into CauHoi values(7, 1, N'Hiện tại Anh/ Chị có các bệnh nào dưới đây', 2, 0, null, N'Thanh Khiết', null, null, 0, 0)
insert into CauHoi values(8, 1, N'Cam kết', 2, 1, null, N'Thanh Khiết', null, null, 0, 0)
insert into CauHoi values(9, 2, N'Số nào lớn nhất trong các số sau: ', 3, 1, null, N'Thanh Khiết', null, null, 1, 0)
insert into CauHoi values(10, 2, N'Số liền sau của 489 là:', 3, 1, null, N'Thanh Khiết', null, null, 1, 0)
insert into CauHoi values(11, 2, N'Chu vi hình chữ vuông có cạnh 4cm là: ', 3, 1, null, N'Thanh Khiết', null, null, 1, 0)
insert into CauHoi values(12, 2, N' 5hm + 7m có kết quả là: ', 3, 1, null, N'Thanh Khiết', null, null, 1, 0)
insert into CauHoi values(13, 2, N'Tính giá trị biểu thức 139 + 603 / 3: ', 1, 1, null, N'Thanh Khiết', null, null, 1, 0)
insert into CauHoi values(14, 2, N'Tìm X với X – 258 = 347: ', 1, 1, null, N'Thanh Khiết', null, null, 1, 0)
insert into CauHoi values(15, 2, N'Cửa hàng gạo có 232kg gạo. Cửa hàng đã bán đi 1/4 số gạo đó. Hỏi cửa hàng còn bao nhiêu ki-lô-gam gạo? ', 1, 1, null, N'Thanh Khiết', null, null, 2, 0)
insert into CauHoi values(16, 2, N'Tìm một số biết rằng. Lấy số đó nhân với số lớn nhất có 1 chữ số thì được 108 ', 1, 1, null, N'Thanh Khiết', null, null, 2, 0)

----SubCauHoi----
select*from Sub_CauHoi
insert into Sub_CauHoi values(1, 1, N'Có')
insert into Sub_CauHoi values(2, 1, N'Không')
insert into Sub_CauHoi values(3, 2, N'Có')
insert into Sub_CauHoi values(4, 2, N'Không')
insert into Sub_CauHoi values(5, 3, N'Có')
insert into Sub_CauHoi values(6, 3, N'Không')
insert into Sub_CauHoi values(7, 5, N'Sốt')
insert into Sub_CauHoi values(8, 5, N'Ho')
insert into Sub_CauHoi values(9, 5, N'Khó thở')
insert into Sub_CauHoi values(10, 5, N'Viêm phổi')
insert into Sub_CauHoi values(11, 5, N'Đau họng')
insert into Sub_CauHoi values(12, 5, N'Mệt mỏi')
insert into Sub_CauHoi values(13, 5, N'Không')
insert into Sub_CauHoi values(14, 6, N'Người bệnh, nghi nhờ mắc bệnh COVID 19')
insert into Sub_CauHoi values(15, 6, N'Người từ nước có bệnh COVID 19')
insert into Sub_CauHoi values(16, 6, N'Người có biểu hiện(sốt, ho, khó thở, viêm phổi)')
insert into Sub_CauHoi values(17, 6, N'Không')
insert into Sub_CauHoi values(18, 7, N'Bệnh máu mãn tính')
insert into Sub_CauHoi values(19, 7, N'Bệnh phổi mãn tính')
insert into Sub_CauHoi values(20, 7, N'Bệnh thận mãn tính')
insert into Sub_CauHoi values(21, 7, N'Bệnh tim mạch')
insert into Sub_CauHoi values(22, 7, N'Huyết áp cao')
insert into Sub_CauHoi values(23, 7, N'HIV hoặc suy giảm miển dịch')
insert into Sub_CauHoi values(24, 7, N'Người nhận ghép tạng, tủy xương')
insert into Sub_CauHoi values(25, 7, N'Tiểu đường')
insert into Sub_CauHoi values(26, 7, N'Ung thư')
insert into Sub_CauHoi values(27, 7, N'Có thai')
insert into Sub_CauHoi values(28, 7, N'Không')
insert into Sub_CauHoi values(29, 8, N'Tôi cảm kết các thông tin khai báo là đúng sự thật')
insert into Sub_CauHoi values(30, 9, N'952')
insert into Sub_CauHoi values(31, 9, N'415')
insert into Sub_CauHoi values(32, 9, N'201')
insert into Sub_CauHoi values(33, 9, N'300')
insert into Sub_CauHoi values(34, 10, N'412')
insert into Sub_CauHoi values(35, 10, N'321')
insert into Sub_CauHoi values(36, 10, N'490')
insert into Sub_CauHoi values(37, 10, N'590')
insert into Sub_CauHoi values(38, 11, N'8cm')
insert into Sub_CauHoi values(39, 11, N'81cm')
insert into Sub_CauHoi values(40, 11, N'34cm')
insert into Sub_CauHoi values(41, 11, N'8m')
insert into Sub_CauHoi values(42, 12, N'507m')
insert into Sub_CauHoi values(43, 12, N'57m')
insert into Sub_CauHoi values(44, 12, N'517m')
insert into Sub_CauHoi values(45, 12, N'7m')

-- Câu Trả Lời Đúng
insert into CauTraLoiDung values(9, N'952')
insert into CauTraLoiDung values(10, N'490')
insert into CauTraLoiDung values(11, N'8cm')
insert into CauTraLoiDung values(12, N'507m')
insert into CauTraLoiDung values(13, N'340')
insert into CauTraLoiDung values(14, N'605')
insert into CauTraLoiDung values(15, N'58')
insert into CauTraLoiDung values(16, N'12')




