namespace Util.Extensions {
	public static class BoolExtension {
		public static void Toggle(this ref bool value) {
			value = !value;
		}
	}
}