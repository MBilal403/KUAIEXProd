

namespace DataAccessLayer.Recources
{
    /// <summary>
    /// Message String Keys.
    /// These keys will be used by the front end to translate.
    /// </summary>
    public static class MsgKeys
    {
        /// <summary>
        /// "DuplicateValueExist Input Parameters."
        /// </summary>
        public static string DuplicateValueExist => "duplicate_value_exist";

        /// <summary>
        /// "Invalid Input Parameters."
        /// </summary>
        public static string InvalidInputParameters => "invalid_input";

        /// <summary>
        /// "No Application Allowed."
        /// </summary>
        public static string NoApplicationAllowed => "no_application_allowed";

        /// <summary>
        /// "Updated Successfully."
        /// </summary>
        public static string UpdatedSuccessfully => "update_success";

        /// <summary>
        /// "Deleted Successfully."
        /// </summary>
        public static string DeletedSuccessfully => "delete_success";

        /// <summary>
        /// "Created Successfully."
        /// </summary>
        public static string CreatedSuccessfully => "insert_success";

        /// <summary>
        /// "Requested object does not exists in database."
        /// </summary>
        public static string ObjectNotExists => "object_not_exists";

        /// <summary>
        /// "Unable to register user."
        /// </summary>
        public static string UserRegistrationFailed => "user_registration_failed";

        /// <summary>
        /// "Login credentials are invalid."
        /// </summary>
        public static string InvalidLoginCredentials => "invalid_login";

        /// <summary>
        /// Login Successfully Msg
        /// </summary>
        public static string LoginSuccessfully => "login_successfully";

        /// <summary>
        /// "Cannot update user."
        /// </summary>
        public static string UserUpdationFailed => "user_updation_failed";

        /// <summary>
        /// "Data you requested is not active."
        /// </summary>
        public static string ObjectNotActive => "not_active";

        /// <summary>
        /// "You are not allowed to perform this operation."
        /// </summary>
        public static string NotAllowed => "not_allowed";

        /// <summary>
        /// "Upload Files Failed. "
        /// </summary>
        public static string uploadFilesFailed => "upload_files_failed";

        /// <summary>
        /// "Email confirmation link sent successfully."
        /// </summary>
        public static string EmailConfirmationLinkSuccess => "email_sent_success";

        /// <summary>
        /// "Unable to send email confirmation link."
        /// </summary>
        public static string EmailConfirmationLinkFailure => "email_sent_failure";

        /// <summary>
        /// "Username is incorrect."
        /// </summary>
        public static string UsernameIsIncorrect => "username_incorrect";

        /// <summary>
        /// "Password is incorrect."
        /// </summary>
        public static string PasswordIsIncorrect => "password_incorrect";

        /// <summary>
        /// "User has been deactivated."
        /// </summary>
        public static string UserDeactivated => "user_not_active";

        /// <summary>
        /// "Email address is not verified."
        /// </summary>
        public static string EmailNotVerified => "email_not_verified";

        /// <summary>
        /// "Token is not valid."
        /// </summary>
        public static string InvalidToken => "invalid_token";

        /// <summary>
        /// Gets the user already exists.
        /// </summary>
        public static string UserAlreadyExists => "user_already_exists";

        /// <summary>
        /// Gets the unknown failure.
        /// </summary>
        public static string UnknownFailure => "unknown_failure";

        /// <summary>
        /// Gets the email is taken.
        /// </summary>
        public static string EmailIsTaken => "email_is_taken";

        /// <summary>
        /// Gets the email is invalid.
        /// </summary>
        public static string EmailIsInvalid => "email_is_invalid";

        /// <summary>
        /// Gets the lockout not enabled.
        /// </summary>
        public static string LockoutNotEnabled => "lockout_not_enabled";

        /// <summary>
        /// Gets the optimistic concurrency failure.
        /// </summary>
        public static string OptimisticConcurrencyFailure => "optimistic_concurrency_failure";

        /// <summary>
        /// Gets the password too short.
        /// </summary>
        public static string PasswordTooShort => "password_too_short";

        /// <summary>
        /// Gets the password must have digit.
        /// </summary>
        public static string PasswordMustHaveDigit => "password_must_have_digit";

        /// <summary>
        /// Gets the password must have lowercase.
        /// </summary>
        public static string PasswordMustHaveLowercase => "password_must_have_lowercase";

        /// <summary>
        /// Gets the password must have non alphanumeric.
        /// </summary>
        public static string PasswordMustHaveNonAlphanumeric => "password_must_have_non_alphanumeric";

        /// <summary>
        /// Gets the password must have uppercase.
        /// </summary>
        public static string PasswordMustHaveUppercase => "password_must_have_uppercase";

        /// <summary>
        /// Gets the role name taken.
        /// </summary>
        public static string RoleNameTaken => "role_name_taken";

        /// <summary>
        /// Gets the role name invalid.
        /// </summary>
        public static string RoleNameInvalid => "role_name_invalid";

        /// <summary>
        /// Gets the user has a password set.
        /// </summary>
        public static string UserHasAPasswordSet => "user_has_a_password_set";

        /// <summary>
        /// Gets the user already in role.
        /// </summary>
        public static string UserAlreadyInRole => "user_already_in_role";

        /// <summary>
        /// Gets the user not in role.
        /// </summary>
        public static string UserNotInRole => "user_not_in_role";

        /// <summary>
        /// Gets the email not found.
        /// </summary>
        public static string EmailNotFound => "email_not_found";

        /// <summary>
        /// Password reset link sent successfully.
        /// </summary>
        public static string PasswordResetSent => "password_reset_sent";

        /// <summary>
        /// Gets the record with same name exists.
        /// </summary>
        public static string RecordWithSameNameExists => "with_same_name_exists";

        /// <summary>
        /// Gets the record with same name exists.
        /// </summary>
        public static string InActiveRecordWithSameNameExists => "inactive_with_same_name_exists";

        /// <summary>
        /// Child entity exists.
        /// </summary>
        public static string ChildEntityExists => "related_entity";

        /// <summary>
        /// Gets the sub nodes level up key.
        /// </summary>
        public static string SubNodesLevelUp => "sub_nodes_level_up";

        /// <summary>
        /// Gets the msgs will be shifted key.
        /// </summary>
        public static string MsgsWillBeShifted => "msgs_will_be_shifted";

        /// <summary>
        /// Gets the msgs please confirm.
        /// </summary>
        public static string PleaseConfirm => "require_confirmation";

        /// <summary>
        /// Gets the msgs User Locked.
        /// </summary>
        public static string UserLocked => "user_is_locked";

        /// <summary>
        /// Gets the msgs Vendor Not Deleted.
        /// </summary>
        public static string VendorNotDeleted => "vendor_not_deleted";

        /// <summary>
        /// vendor not updated.
        /// </summary>
        public static string VendorNotUpdated => "vendor_not_updated";

        /// <summary>
        /// Your credentials are not verified from active directory.
        /// </summary>
        public static string ADCredentialsNotVerified => "ad_credentials_not_verified";

        /// <summary>
        /// User belongs to active directory..
        /// </summary>
        public static string ActiveDirectoryUser => "active_directory_user";

        /// <summary>
        /// Gets the msgs PasswordSetSuccessfully.
        /// </summary>
        public static string PasswordSetSuccessfully => "password_has_been_set_successfully";

        /// <summary>
        /// Gets the msgs FailedToGetPdf.
        /// </summary>
        public static string FailedToGetFile => "failed_to_get_pdf";

        /// <summary>
        /// Failed T oDelete File from Server
        /// </summary>
        public static string FailedToDeleteFile => "failed_to_delete_file";

        /// Gets the Parent Sibling Associated.
        /// </summary>
        public static string ParentSiblingAssociated => "parent_sibling_associated";

        /// <summary>
        /// Gets the CaptchNotVerified.
        /// </summary>
        public static string CaptchNotVerified => "captch_not_verified";

        /// <summary>
        /// Gets the GoogleAuthCodeNotVerified.
        /// </summary>
        public static string GoogleAuthCodeNotVerified => "google_auth_code_not_verified";

        /// <summary>
        /// Gets the ResponseTimeOut.
        /// </summary>
        public static string ResponseTimeOut => "response_time_out";

        /// <summary>
        /// Gets the InCorrectStartDate.
        /// </summary>
        public static string InCorrectStartDate => "in_correct_start_date";

        /// <summary>
        /// Gets the InvalidRegistrationImageCount.
        /// </summary>
        public static string InvalidRegistrationImageCount => "invalid_registration_image_count";

        /// <summary>
        /// Gets the InvalidQuestionObject.
        /// </summary>
        public static string InvalidQuestionObject => "invalid_question_object";

        /// <summary>
        /// Gets the EmailNotSend.
        /// </summary>
        public static string EmailNotSend => "email_not_send";

        /// <summary>
        /// Gets the NoShutterBFound.
        /// </summary>
        public static string NoShutterBAvailableInThisLocation => "no_shutterB_available_in_this_location";
        /// Gets the EmailNotMatched.
        /// </summary>
        public static string EmailNotMatched => "email_not_matched";

        /// <summary>
        /// Gets the NoIdExist.
        /// </summary>
        public static string NoIdExist => "no_id_exist";

        /// <summary>
        /// Get the NoFileExistInRequest
        /// </summary>
        public static string Error => "error";
        /// <summary>
        /// "Something went wrong."
        /// </summary>
        public static string SomethingWentWrong => "something_went_wrong";
    }
}
