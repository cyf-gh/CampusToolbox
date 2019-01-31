namespace CTB.Model.Account {
    public enum Grade {
        TS, JCS, // Technical school student, junior college student

        /* Undergraduate Student */
        UG1,
        UG2,
        UG3,
        UG4,

        /* Postgraduate Student */
        PG1,
        PG2,
        PG3,
        PG4,

        /* Doctoral Student */
        D1,
        D2,
        D3,
        D4,

        BD, // Bachelor Degree
        MD, // Master Degree
        DD, // Doctor Degree
    }

    public class _SharedAccountModel {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string College { get; set; }
    }
}
