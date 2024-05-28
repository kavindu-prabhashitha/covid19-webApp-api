namespace covid19_api.Constants
{
    public enum Permissions
    {
        ADD_USER_DATA,
        UPDATE_USER_DATA,
        DELETE_USER_DATA,
        GET_USER_DATA,

        //Super Admin Previelleges
        CREATE_USER_ACCOUNT,
        CREATE_ADMIN_ACCOUNT,
        CREATE_ROLE,
        DELETE_ROLE,
        UPDATE_ROLE,

        CREATE_PERMISSION,
        DELETE_PERMISSION,
        GET_PERMISSIONS,
        UPDATE_PERMISSION,

        //Covid19 Data
        SAVE_COVID19_CASE,
        SAVE_COVID19_COUNTRY,
        UPDATE_COVID19_COUNTRY_CASE

    }
}
