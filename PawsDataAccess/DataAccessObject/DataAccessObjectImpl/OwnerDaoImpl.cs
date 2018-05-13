using PawsDataAccess.Database;
using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using static PawsDataAccess.Constant.Owner;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class OwnerDaoImpl : IOwnerDao
    {
        private const string ID_COLUMN = "Id";
        private const string USERNAME_COLUMN = "Username";
        private const string PASSWORD_COLUMN = "Password";
        private const string NAME_COLUMN = "Name";
        private const string LAST_NAME_COLUMN = "LastName";
        private const string BIRTHDATE_COLUMN = "BirthDate";
        private const string DNI_COLUMN = "DNI";
        private const string EMAIL_COLUMN = "EMail";
        private const string ADDRESS_COLUMN = "Address";
        private const string PHONE_COLUMN = "PhoneNumber";
        private const string PROFILE_PIC_COLUMN = "ProfilePicture";
        private const string DISTRICT_ID_COLUMN = "DistrictId";

        private const string ID_PARAM = "@id";
        private const string USERNAME_PARAM = "@user";
        private const string PASSWORD_PARAM = "@pass";
        private const string NAME_PARAM = "@name";
        private const string LAST_NAME_PARAM = "@lastName";
        private const string BIRTHDATE_PARAM = "@birthDate";
        private const string DNI_PARAM = "@dni";
        private const string EMAIL_PARAM = "@email";
        private const string ADDRESS_PARAM = "@address";
        private const string PHONE_PARAM = "@phoneNum";
        private const string PROFILE_PIC_PARAM = "@profilePic";
        private const string DISTRICT_ID_PARAM = "@disId";
        private const string ROW_COUNT_PARAM = "@rowCount";

        IDatabase db;
        IDbCommand cmd;
        IDataReader dr;

        public OwnerDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(Owner toInsert, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_OWNER_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(USERNAME_PARAM, DaoUtil.ValueOrDbNull(toInsert.Username)));
                cmd.Parameters.Add(db.GetParameter(PASSWORD_PARAM, DaoUtil.ValueOrDbNull(toInsert.Password)));
                cmd.Parameters.Add(db.GetParameter(NAME_PARAM, DaoUtil.ValueOrDbNull(toInsert.Name)));
                cmd.Parameters.Add(db.GetParameter(LAST_NAME_PARAM, DaoUtil.ValueOrDbNull(toInsert.LastName)));
                cmd.Parameters.Add(db.GetParameter(BIRTHDATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.BirthDate)));
                cmd.Parameters.Add(db.GetParameter(DNI_PARAM, DaoUtil.ValueOrDbNull(toInsert.DNI)));
                cmd.Parameters.Add(db.GetParameter(EMAIL_PARAM, DaoUtil.ValueOrDbNull(toInsert.EMail)));
                cmd.Parameters.Add(db.GetParameter(ADDRESS_PARAM, DaoUtil.ValueOrDbNull(toInsert.Address)));
                cmd.Parameters.Add(db.GetParameter(PHONE_PARAM, DaoUtil.ValueOrDbNull(toInsert.PhoneNumber)));
                cmd.Parameters.Add(db.GetParameter(PROFILE_PIC_PARAM, DaoUtil.ValueOrDbNull(toInsert.ProfilePicture)));
                cmd.Parameters.Add(db.GetParameter(DISTRICT_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.DistrictId)));
                cmd.Parameters.Add(db.GetOutputParameter(ID_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ID_PARAM]).Value;
            }
        }

        public bool Update(Owner toUpdate, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_OWNER_UPDATE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Id)));
                cmd.Parameters.Add(db.GetParameter(USERNAME_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Username)));
                cmd.Parameters.Add(db.GetParameter(PASSWORD_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Password)));
                cmd.Parameters.Add(db.GetParameter(NAME_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Name)));
                cmd.Parameters.Add(db.GetParameter(LAST_NAME_PARAM, DaoUtil.ValueOrDbNull(toUpdate.LastName)));
                cmd.Parameters.Add(db.GetParameter(BIRTHDATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.BirthDate)));
                cmd.Parameters.Add(db.GetParameter(DNI_PARAM, DaoUtil.ValueOrDbNull(toUpdate.DNI)));
                cmd.Parameters.Add(db.GetParameter(EMAIL_PARAM, DaoUtil.ValueOrDbNull(toUpdate.EMail)));
                cmd.Parameters.Add(db.GetParameter(ADDRESS_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Address)));
                cmd.Parameters.Add(db.GetParameter(PHONE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.PhoneNumber)));
                cmd.Parameters.Add(db.GetParameter(PROFILE_PIC_PARAM, DaoUtil.ValueOrDbNull(toUpdate.ProfilePicture)));
                cmd.Parameters.Add(db.GetParameter(DISTRICT_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.DistrictId)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public bool Delete(object id, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_OWNER_DELETE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public Owner Find(object id, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_OWNER_FIND, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                using (dr = cmd.ExecuteReader())
                {
                    int ID_COLUMN_INDEX = dr.GetOrdinal(ID_COLUMN);
                    int USERNAME_COLUMN_INDEX = dr.GetOrdinal(USERNAME_COLUMN);
                    int PASSWORD_COLUMN_INDEX = dr.GetOrdinal(PASSWORD_COLUMN);
                    int NAME_COLUMN_INDEX = dr.GetOrdinal(NAME_COLUMN);
                    int LAST_NAME_COLUMN_INDEX = dr.GetOrdinal(LAST_NAME_COLUMN);
                    int BIRTHDATE_COLUMN_INDEX = dr.GetOrdinal(BIRTHDATE_COLUMN);
                    int DNI_COLUMN_INDEX = dr.GetOrdinal(DNI_COLUMN);
                    int EMAIL_COLUMN_INDEX = dr.GetOrdinal(EMAIL_COLUMN);
                    int ADDRESS_COLUMN_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                    int PHONE_COLUMN_INDEX = dr.GetOrdinal(PHONE_COLUMN);
                    int PROFILE_PIC_COLUMN_INDEX = dr.GetOrdinal(PROFILE_PIC_COLUMN);
                    int DISTRICT_ID_COLUMN_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);

                    Owner owner = null;
                    if (dr.Read())
                    {
                        owner = new Owner
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_COLUMN_INDEX, dr),
                            Username = DaoUtil.ValueOrDefault<string>(USERNAME_COLUMN_INDEX, dr),
                            Password = DaoUtil.ValueOrDefault<string>(PASSWORD_COLUMN_INDEX, dr),
                            Name = DaoUtil.ValueOrDefault<string>(NAME_COLUMN_INDEX, dr),
                            LastName = DaoUtil.ValueOrDefault<string>(LAST_NAME_COLUMN_INDEX, dr),
                            BirthDate = DaoUtil.ValueOrDefault<DateTime>(BIRTHDATE_COLUMN_INDEX, dr),
                            DNI = DaoUtil.ValueOrDefault<string>(DNI_COLUMN_INDEX, dr),
                            EMail = DaoUtil.ValueOrDefault<string>(EMAIL_COLUMN_INDEX, dr),
                            Address = DaoUtil.ValueOrDefault<string>(ADDRESS_COLUMN_INDEX, dr),
                            PhoneNumber = DaoUtil.ValueOrDefault<string>(PHONE_COLUMN_INDEX, dr),
                            ProfilePicture = DaoUtil.ValueOrDefault<string>(PROFILE_PIC_COLUMN_INDEX, dr),
                            DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_COLUMN_INDEX, dr)
                        };
                    }
                    return owner;
                }
            }
        }

        public List<Owner> FindAll(IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_OWNER_FINDALL, conn))
            using (dr = cmd.ExecuteReader())
            {
                int ID_COLUMN_INDEX = dr.GetOrdinal(ID_COLUMN);
                int USERNAME_COLUMN_INDEX = dr.GetOrdinal(USERNAME_COLUMN);
                int PASSWORD_COLUMN_INDEX = dr.GetOrdinal(PASSWORD_COLUMN);
                int NAME_COLUMN_INDEX = dr.GetOrdinal(NAME_COLUMN);
                int LAST_NAME_COLUMN_INDEX = dr.GetOrdinal(LAST_NAME_COLUMN);
                int BIRTHDATE_COLUMN_INDEX = dr.GetOrdinal(BIRTHDATE_COLUMN);
                int DNI_COLUMN_INDEX = dr.GetOrdinal(DNI_COLUMN);
                int EMAIL_COLUMN_INDEX = dr.GetOrdinal(EMAIL_COLUMN);
                int ADDRESS_COLUMN_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                int PHONE_COLUMN_INDEX = dr.GetOrdinal(PHONE_COLUMN);
                int PROFILE_PIC_COLUMN_INDEX = dr.GetOrdinal(PROFILE_PIC_COLUMN);
                int DISTRICT_ID_COLUMN_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);

                List<Owner> lOwner = new List<Owner>();
                Owner owner = null;
                while (dr.Read())
                {
                    owner = new Owner
                    {
                        Id = DaoUtil.ValueOrDefault<int>(ID_COLUMN_INDEX, dr),
                        Username = DaoUtil.ValueOrDefault<string>(USERNAME_COLUMN_INDEX, dr),
                        Password = DaoUtil.ValueOrDefault<string>(PASSWORD_COLUMN_INDEX, dr),
                        Name = DaoUtil.ValueOrDefault<string>(NAME_COLUMN_INDEX, dr),
                        LastName = DaoUtil.ValueOrDefault<string>(LAST_NAME_COLUMN_INDEX, dr),
                        BirthDate = DaoUtil.ValueOrDefault<DateTime>(BIRTHDATE_COLUMN_INDEX, dr),
                        DNI = DaoUtil.ValueOrDefault<string>(DNI_COLUMN_INDEX, dr),
                        EMail = DaoUtil.ValueOrDefault<string>(EMAIL_COLUMN_INDEX, dr),
                        Address = DaoUtil.ValueOrDefault<string>(ADDRESS_COLUMN_INDEX, dr),
                        PhoneNumber = DaoUtil.ValueOrDefault<string>(PHONE_COLUMN_INDEX, dr),
                        ProfilePicture = DaoUtil.ValueOrDefault<string>(PROFILE_PIC_COLUMN_INDEX, dr),
                        DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_COLUMN_INDEX, dr)
                    };
                    lOwner.Add(owner);
                }
                return lOwner;
            }
        }

        public Owner Login(Owner owner, IDbConnection conn)
        {
            using (cmd = db.GetCommand(USP_OWNER_LOGIN, conn))
            {
                cmd.Parameters.Add(db.GetParameter(USERNAME_PARAM, owner.Username));
                cmd.Parameters.Add(db.GetParameter(PASSWORD_PARAM, owner.Password));

                using (dr = cmd.ExecuteReader())
                {
                    int ID_COLUMN_INDEX = dr.GetOrdinal(ID_COLUMN);
                    int USERNAME_COLUMN_INDEX = dr.GetOrdinal(USERNAME_COLUMN);
                    int PASSWORD_COLUMN_INDEX = dr.GetOrdinal(PASSWORD_COLUMN);
                    int NAME_COLUMN_INDEX = dr.GetOrdinal(NAME_COLUMN);
                    int LAST_NAME_COLUMN_INDEX = dr.GetOrdinal(LAST_NAME_COLUMN);
                    int BIRTHDATE_COLUMN_INDEX = dr.GetOrdinal(BIRTHDATE_COLUMN);
                    int DNI_COLUMN_INDEX = dr.GetOrdinal(DNI_COLUMN);
                    int EMAIL_COLUMN_INDEX = dr.GetOrdinal(EMAIL_COLUMN);
                    int ADDRESS_COLUMN_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                    int PHONE_COLUMN_INDEX = dr.GetOrdinal(PHONE_COLUMN);
                    int PROFILE_PIC_COLUMN_INDEX = dr.GetOrdinal(PROFILE_PIC_COLUMN);
                    int DISTRICT_ID_COLUMN_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);
                    
                    if (dr.Read())
                    {
                        owner = new Owner
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_COLUMN_INDEX, dr),
                            Username = DaoUtil.ValueOrDefault<string>(USERNAME_COLUMN_INDEX, dr),
                            Password = DaoUtil.ValueOrDefault<string>(PASSWORD_COLUMN_INDEX, dr),
                            Name = DaoUtil.ValueOrDefault<string>(NAME_COLUMN_INDEX, dr),
                            LastName = DaoUtil.ValueOrDefault<string>(LAST_NAME_COLUMN_INDEX, dr),
                            BirthDate = DaoUtil.ValueOrDefault<DateTime>(BIRTHDATE_COLUMN_INDEX, dr),
                            DNI = DaoUtil.ValueOrDefault<string>(DNI_COLUMN_INDEX, dr),
                            EMail = DaoUtil.ValueOrDefault<string>(EMAIL_COLUMN_INDEX, dr),
                            Address = DaoUtil.ValueOrDefault<string>(ADDRESS_COLUMN_INDEX, dr),
                            PhoneNumber = DaoUtil.ValueOrDefault<string>(PHONE_COLUMN_INDEX, dr),
                            ProfilePicture = DaoUtil.ValueOrDefault<string>(PROFILE_PIC_COLUMN_INDEX, dr),
                            DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_COLUMN_INDEX, dr)
                        };
                    }
                    return owner;
                }
            }
        }
    }
}
