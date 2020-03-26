DELIMITER $$

USE `staybazar_data`$$

DROP PROCEDURE IF EXISTS `report_PropertyDetails_ExcelJob`$$

CREATE DEFINER=`dev`@`%` PROCEDURE `report_PropertyDetails_ExcelJob`(pSearchString VARCHAR(100),pSearchvalue INT)
BEGIN
          SELECT    REPLACE( b.Name,'	','.') AS Supplier_Business_Name, REPLACE( b.UserCode,'	','.') AS UserCode,REPLACE(u.Email,'	','.') AS Login_ID,
u.Email AS Supplier_Email_ID,CONCAT(u.Firstname) AS Supplier_Contact_Name,
REPLACE(ua.Address,'	','.') AS Supplier_Address,
ua.Phone AS Supplier_Phone,ua.Mobile AS SupplierMobile, ua.City AS  Supplier_City , s.Name AS Supplier_State,
 (CASE WHEN p.status = 2 THEN 'Disabled'
WHEN p.status = 4 THEN 'Deleted'
ELSE
'Enabled' END )  AS Property_Status,
p.Title AS Property_Name, REPLACE(p.Address,'	','') AS Property_Address, 
p.`B2CMarkupShortTerm`,p.`B2CMarkupLongTerm`,p.`B2BMarkupShortTerm`,p.`B2BMarkupLongTerm`,
p.B2BStdShortTermDis AS StandardDiscountshortterm,p.B2BStdLongTermDis AS StandardDiscountlongterm,
p.B2BStdLongTermDis  AS CorporateDiscountslongterm,p.B2BStdShortTermDis AS CorporateDiscountshortterm,
pc.Name AS Property_City, p.location AS Property_Location, ps.Name AS Property_State, 
u.CreatedDate AS Joined_Date,
(CASE WHEN a.status = 2 THEN 'Disabled'
WHEN a.status = 4 THEN 'Deleted'
ELSE
'Enabled' END ) AS Accommodation_Status,a.description   AS  Accommodation_Description,
(SELECT DISTINCT sc.Title FROM staycategory sc WHERE sc.CategoryId = a.StayCategoryId)  AS Accommodation_category,
 aty.Title AS AccommodationType,
    (SELECT GROUP_CONCAT(iv.`Quantity`) FROM `inventory` iv WHERE iv.`AccommodationId`=a.AccommodationId  ) AS Quantity,
  (SELECT GROUP_CONCAT(r.DailyRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate` ) AS DailyRate,
  (SELECT GROUP_CONCAT(r.WeeklyRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate` ) AS WeeklyRate,
   (SELECT GROUP_CONCAT(r.MonthlyRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate`) AS MonthlyRate,
    (SELECT GROUP_CONCAT(r.LongTermRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate`) AS LongTermRate,
  (SELECT GROUP_CONCAT(r.GuestRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate` ) AS GuestRate,
    r.StartDate AS RateStartDate ,
    r.EndDate AS RateEndDate,
(SELECT FirstName FROM `user` WHERE UserId = r.UpdatedBy) AS UpdatedBy,
r.UpdatedDate AS UpdatedOn,
a.Bedrooms,a.TotalAccommodations AS SupplierTotalAccommodations,a.MaxNoOfPeople AS Accommodation_MaxPeople,a.MinNoofPeople AS Adults,a.MaxNoOfChildren AS Children,
(SELECT GROUP_CONCAT(r.CalcDailyRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate` ) AS DailyRate,
(SELECT GROUP_CONCAT(r.CalcWeeklyRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate` )  AS WeeklyRate,
(SELECT GROUP_CONCAT(r.CalcMonthlyRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate` ) AS MonthlyRate,
(SELECT GROUP_CONCAT(r.CalcLongTermRate) FROM rates r WHERE r.`AccommodationId`=a.AccommodationId AND r.ratefor =7  ORDER BY `EndDate` ) AS LongTermRate,'' AS MealPlan,
(SELECT GROUP_CONCAT(pfe.Title)   FROM propertyfeature pfe INNER JOIN   property_propertyfeature ppfe ON pfe.PropertyFeatureId =ppfe.PropertyFeatureId  WHERE ppfe.PropertyId =p.PropertyId ORDER BY pfe.Title) AS Complementary_itemsfacilities,
'' AS GuestRate,
(SELECT FirstName FROM `user` WHERE UserId = r.UpdatedBy) AS UpdatedBy,
r.UpdatedDate AS UpdatedOn,
b.PANNo AS PANNumber,b.VATRegNo AS VATNumber,
(CASE WHEN p.CancellationType <> 1 THEN 
'Fixed Night'
ELSE 
'Fixed Percent'
END ) 
AS CancellationPolicy,
p.CancellationPeriod AS Cancellationperiod,p.CancellationCharge AS cancellationcharges,
(CASE WHEN p.status <> 1 THEN 'NO'
ELSE
'YES' END ) AS Property_Enabled,
(SELECT COUNT(*) FROM property_propertyfeature pf WHERE  p.propertyId = pf.`PropertyId`) AS PROP_FEAT_SET,
(SELECT COUNT(*) FROM propertyfiles pfl WHERE propertyid= p.propertyId) AS PROP_PICTURE,
(SELECT COUNT(*) FROM accommodation_feature af WHERE a.accommodationid = af.accommodationId ) AS ACC_Feature_SET,
(SELECT COUNT(*) FROM accommodationfiles af WHERE a.accommodationid = af.accommodationid ) AS ACC_PICTURE,
u.LastLoginOn AS Last_login_Date,ba.AccountName AS AccountName,ba.AccountNumber AS BankAccountNumber,ba.BankName AS NameoftheBank,ba.BranchAddress AS BranchoftheBank,
ba.RTGSNumber AS IFSC,ba.MIRCCode AS MICR,
(SELECT b.BookingDate FROM booking b INNER JOIN bookingitems bi ON b.bookingid = bi.bookingid
 WHERE bi.AccommodationId =a.AccommodationId ORDER BY BookingDate DESC LIMIT 1) AS LastBookingDate,
 (SELECT bi.CheckIn FROM booking b INNER JOIN bookingitems bi ON b.bookingid = bi.bookingid
 WHERE bi.AccommodationId =a.AccommodationId ORDER BY BookingDate DESC LIMIT 1) AS CheckIn,
 (SELECT bi.CheckOut FROM booking b INNER JOIN bookingitems bi ON b.bookingid = bi.bookingid
 WHERE bi.AccommodationId =a.AccommodationId ORDER BY BookingDate DESC LIMIT 1) AS CheckOut,
 
(SELECT GROUP_CONCAT(tt.Title,'-',(pt.TaxValue)) FROM propertytax pt INNER JOIN taxtitle tt ON tt.TaxTitleId =pt.TaxTitle WHERE pt.PropertyId= p.PropertyId) AS TaxTitle,
p.ChildAgeLimit AS ChildAgeLimit,p.Rating AS Rating,
'' AS Name_0f_the_person_visited_for_quality_check,
'' AS Date_of_the_visit_for_the_quality_check,
p.PageTitle AS PageTitle,p.MetaDescription AS MetaDescription,p.DistanceFromCity AS DistanceFromCity,
(SELECT GROUP_CONCAT(lm.Landmark) FROM  landmarks lm WHERE lm.PropertyId =p.PropertyId ORDER BY lm.range) AS LandMark,'' AS AnyOtherProminanceNearby
 FROM `user` u INNER JOIN b2b b ON u.UserId = b.B2BId INNER JOIN Address ua ON u.UserId = ua.Userid
INNER JOIN property p ON u.Userid = p.OwnerId INNER JOIN State s ON ua.State = s.StateId 
INNER JOIN State ps ON p.State = ps.StateId INNER JOIN City pc ON p.CityId = pc.CityId
INNER JOIN accommodation a ON a.PropertyId = p.PropertyId
INNER JOIN inventory iv  ON a.accommodationId = iv.accommodationId
INNER JOIN accommodationtype aty ON aty.accommodationtypeid = a.accommodationtypeid
INNER JOIN rates r ON a.accommodationId = r.accommodationId
LEFT JOIN bankaccount ba ON ba.UserId =u.UserId
WHERE p.status <> 4 AND
(
		(pSearchvalue = 1 AND ((p.title LIKE CONCAT('%',pSearchString,'%')) OR ( b.name LIKE CONCAT('%',pSearchString,'%') )))
		OR
		(pSearchvalue = 2 AND (p.city LIKE CONCAT('%',pSearchString,'%') ))
		OR
		(pSearchvalue = 3 AND (p.location LIKE CONCAT('%',pSearchString,'%')))
		OR
		(pSearchvalue = 4 AND ((SELECT sc.`Title` FROM `staycategory` sc WHERE sc.`CategoryId` =a.StayCategoryId) LIKE CONCAT('%',pSearchString,'%')))
		OR
		(pSearchvalue = 5 AND ((SELECT sc.`Title` FROM `accommodationtype` sc WHERE sc.`AccommodationTypeId` =a.`AccommodationTypeId`) LIKE CONCAT('%',pSearchString,'%')))	
		OR
		(pSearchvalue = 0) 		
) 
ORDER BY  u.Userid,ps.Name,b.Name,p.Title ;
    END$$

DELIMITER ;