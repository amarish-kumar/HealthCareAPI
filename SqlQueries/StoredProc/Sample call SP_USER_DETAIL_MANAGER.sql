DECLARE @XmlVal XML=
'<UserSignUp>
		<Id>0</Id>
		<FirstName>Kunal</FirstName>
		<LastName>Mehta</LastName>
		<EmailAddress>kunalsmehtajobs@gmail.com</EmailAddress>
		<Address></Address>
		<PhoneNumber>+919619645344</PhoneNumber>
		<Gender>1</Gender>
		<DOB>25-Apr-1980</DOB>
		<Password>10dulkar</Password>
		<AlternateNo>9833704849</AlternateNo>
		<EmergencyContactNo>9004843363</EmergencyContactNo>
		<EmergencyContactPerson>Mehta</EmergencyContactPerson>
		<DLNumber>1234567890</DLNumber>
		<SSN>1234SD1222</SSN>
		<Active>1</Active>
		<UserType>1</UserType>
	</UserSignUp>'

EXEC SP_USER_DETAIL_MANAGER @XmlVal,NULL,1 --TO INSERT/UPDATE RECORD
EXEC SP_USER_DETAIL_MANAGER @XmlVal,'PASSWORD_RESET',1 --TO RESET PASSWORD
EXEC SP_USER_DETAIL_MANAGER @XmlVal,'DISABLE_USER',1 --TO DISABLE USER