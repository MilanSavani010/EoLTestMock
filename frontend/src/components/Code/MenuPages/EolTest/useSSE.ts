import { useEffect, useState } from "react";

interface UseSSEConfig<T>
{
    parseData?: (data:any) => data is T;

}

export const useSSE = <T extends object>(url: string, config: UseSSEConfig<T>={}): {
    data: T[];
    error: string | null
} => {

    const [data, setData] = useState<T[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const connect = () => {
            const eventSource = new EventSource(url);

            eventSource.onmessage = (event) => {
                try {
                    const rawData = event.data;
                    const parsedData = config.parseData ? config.parseData(rawData): JSON.parse(rawData);
                    setData((prevData) => [...prevData,parsedData]);
                }catch(error)
                {

                }   
            }

            eventSource.onerror = () => {
                console.error("SSE connection failed.");
                eventSource.close();
            };

            return () => {
                eventSource.close();
            }
        }

        const cleanup = connect();
        return cleanup;
    },[url,config]);

    return { data, error };

};