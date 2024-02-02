import urlJoin from "url-join";


const apiPathName = "api";

const auctionApiPath = "auctions";
const uploadJsonPath = "uploadJson"
const loadDataPath = "loadData";

export const getApi = () => {
    const { origin } = window.location;

    const api = {
        auction: {
            uploadFile: async (form: FormData) => {
                const uploadUrl = urlJoin(origin, apiPathName, auctionApiPath, uploadJsonPath);

                return await fetch(uploadUrl, {
                    method: "POST",
                    body: form,
                });
            },
            loadData: async (search: string) => {
                const loadUrl = urlJoin(origin, apiPathName, auctionApiPath, loadDataPath, search);

                return await fetch(loadUrl, {
                    method: "GET",
                }).then(response => {
                    return response.json();
                });
            }
        }
    };

    return api;
}