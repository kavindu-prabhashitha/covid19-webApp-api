namespace covid19_api.Constants
{
    public enum Permissions
    {
        ADD_USER_DATA =1,
        UPDATE_USER_DATA =2,
        DELETE_USER_DATA =3,
        GET_USER_DATA =4,

        //Super Admin Previelleges
        CREATE_USER_ACCOUNT =5,
        CREATE_ADMIN_ACCOUNT =6,
        CREATE_ROLE =7,
        DELETE_ROLE =8,
        UPDATE_ROLE =9,

        CREATE_PERMISSION =10,
        DELETE_PERMISSION =11,
        GET_PERMISSIONS =12,
        UPDATE_PERMISSION =13,

        //Covid19 Data
        SAVE_COVID19_CASE = 14,
        SAVE_COVID19_COUNTRY = 15,
        UPDATE_COVID19_COUNTRY_CASE =16

    }
}
