import * as React from 'react';
import { useState } from 'react';
import { getApi } from './api';

function App() {
    const api = getApi();

    const [file, setFile] = useState<File>()
    const searcRef = React.useRef<HTMLInputElement>();

    const handleChange = (event: any) => {
        setFile(event.target.files[0]);
    };

    const handleSubmit = (event: any) => {
        event.preventDefault();

        if (!file) return;

        const formData = new FormData();

        formData.append("file", file);
        api.auction.uploadFile(formData)
            .then(response => {
                console.log(response);
                alert("Файл успешно загружен и обработан! Невероятный успех!");
            })
            .catch(response => {
                console.log(response);
                alert("Во время загрузки файла произошла ошибка!")
            });
    };

    const loadData = () => {
        api.auction.loadData(searcRef?.current?.value === "" ? "123" : searcRef?.current?.value)
            .then(data => {
                console.log(data);
                alert("Запрос выполнен! Результат смотреть в консоли");
            })
            .catch(response => {
                console.log(response);
                alert("Не удалось выполнить запрос")
            });
    }

    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
            <div style={{ backgroundColor: '#d7d2d2', padding: '10px', borderRadius:'10px' }}>
                <form onSubmit={handleSubmit}>
                    <div style={{ textAlign: 'center', fontSize: '20px' }}>{'Загрузка JSON файла'}</div>
                    <br />
                    <input id="custom-file-upload" type="file" accept="application/json" onChange={handleChange} />
                    <button type="submit">Загрузить</button>
                </form>
                <br />
                
                <br />
                <div style={{ display: 'flex', gap: '3px', alignItems:'center' }}>
                    <input style={{ width: '100%', height:'auto'}} type="text" ref={searcRef} placeholder="Текст запроса"></input>
                    <button style={{ width: 'auto', whiteSpace:'nowrap' }} onClick={loadData}>Выполнить запрос</button>
                </div>
            </div>
        </div>
    );
}
export default App;