using System.Collections.Generic;


// siis: CW:n on toteutettava tuo rajapinta, ja sillä on oltava parametriton konstruktori
public static class WrapperPool<C, CW> where CW : ISettableComponent<C>, new() {
    private static Dictionary<C, CW> wraps = new Dictionary<C, CW>();

    public static CW WrapComponent(C c) {
        if (!wraps.ContainsKey(c)) {
            CW cw = new CW();
            wraps.Add(c, cw);
            cw.SetComponent(c);
        }
        return wraps[c];
    }
}

// koska rajapinnassa ei voi määritellä konstruktoreja, määritellään sitten setteri
public interface ISettableComponent<C> {
    void SetComponent(C c);
}





