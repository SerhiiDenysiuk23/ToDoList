
const baseURL = "https://localhost:7036/graphql"

export const request = async (query: string, variables?: any) => {
    const dbType = (document.cookie.split("; ").find(row => row.startsWith("dbType="))?.split("=")[1]) ?? "SQL"

    return (await fetch(baseURL, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Data-Source': dbType
        },
        body: JSON.stringify({query, variables})
    })).json()
}