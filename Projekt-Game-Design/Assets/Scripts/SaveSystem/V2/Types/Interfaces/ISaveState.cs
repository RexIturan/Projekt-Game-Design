namespace SaveSystem.V2.Data {
	public interface ISaveState<T> {
		public T Save();
		public void Load(T data);
	}
	
	public interface ISaveState {
		public void Save();
		public void Load();
	}
}