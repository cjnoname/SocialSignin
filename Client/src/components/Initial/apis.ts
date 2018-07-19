import server from 'utils/server';

export const getInitialValues = <T>() => server.get<T>('Initial/GetInitialValues');
