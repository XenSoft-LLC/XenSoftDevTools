using XenDB.XenDriver.Service;

namespace XenDB.XenDriver.Factory {
    public static class ServiceFactory {

        public static AuthUserService AuthUserService { get; } = new AuthUserService();

    }
}
