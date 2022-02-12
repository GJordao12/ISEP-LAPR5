import React, {useEffect, useState} from 'react';
import './style.css';
import * as THREE from "three";

function Minimap() {


    const {useRef, useEffect, useState} = React
    const mount = useRef(null)
    const [isAnimating, setAnimating] = useState(true)
    useRef(null);
    const tokenID = sessionStorage.getItem('token');
    const userTokenID = JSON.parse(tokenID);

    const getInfo = async () => {
        const items = await fetch('api/RedesConexoes/'.concat(userTokenID) + '/niveis=0', {})
        return await items.json();
    }

    useEffect(async () => {
        let width = mount.current.clientWidth
        let height = mount.current.clientHeight
        let frameId

        const scene = new THREE.Scene()
        scene.background = null;
        const camera = new THREE.PerspectiveCamera(75, width / height, 0.1, 1000)
        const renderer = new THREE.WebGLRenderer( { alpha: true } ); // init like this
        renderer.setClearColor(0xFF0000, 0);

        let radius = 1;
        const geometry = new THREE.CircleGeometry(radius, 100);

        let material, circle;
        // aqui vai ser para adicionar o user principal
        material = new THREE.MeshBasicMaterial({color: 'red', opacity: 1})
        circle = new THREE.Mesh(geometry, material);
        circle.position.x = 0;
        circle.position.y = 0;

        scene.add(circle);

        let raioAtual = radius;
        const diametroCirculo = radius * 2;
        const espacamento = radius + 0.3;

        let infoMap = await getInfo();
        let myMap = new Map(Object.entries(infoMap));

        let username_coordenadas = new Map();
        let position = {
            positionx: 0,
            positiony: 0
        };
        username_coordenadas.set(userTokenID, position);

        for (let niveis = 1; niveis <= myMap.size; niveis++) {

            let tamanho = radius - (niveis / 10);

            if (tamanho < 0.2) {
                tamanho = 0.2;
            }

            let geometry = new THREE.CircleGeometry(tamanho, 100);

            let n = myMap.get('' + niveis).length; // numero de círculos para cada nível

            let tamanhoTotalNecessario = n * diametroCirculo + (espacamento * n); //Perimetro
            let raioMinimo = ((tamanhoTotalNecessario / Math.PI) / 2); // d = perimetro / pi

            if (raioMinimo <= raioAtual + diametroCirculo) {
                raioMinimo = raioAtual + diametroCirculo + espacamento;
            }

            for (let i = 0; i < n; i++) {

                let id = myMap.get('' + niveis)[i].secundario.id;

                let flag = true;

                if (username_coordenadas.get(id) == null) {
                    flag = false;
                    let x = (raioMinimo * Math.round(Math.cos((360 / n * i) * (Math.PI / 180))));
                    let y = (raioMinimo * Math.round(Math.sin((360 / n * i) * (Math.PI / 180))));

                    material = new THREE.MeshBasicMaterial({color: '#dbb587', opacity: 1});
                    circle = new THREE.Mesh(geometry, material);



                    circle.position.x = x;
                    circle.position.y = y;

                    let position = {
                        nivel: niveis,
                        positionx: circle.position.x,
                        positiony: circle.position.y
                    };

                    scene.add(circle);
                    username_coordenadas.set(id, position);
                }

                //adicionar ligação
                let points = [];

                let userLigado = myMap.get('' + niveis)[i].principal.value;
                let positionUserLigado = username_coordenadas.get(userLigado);
                let positionXUserligado = positionUserLigado.positionx;
                let positionYUserligado = positionUserLigado.positiony;
                let tamanhoUserLigado = radius - ((niveis - 1) / 10);
                if (tamanhoUserLigado < 0.2) {
                    tamanhoUserLigado = 0.2;
                }

                let positionXUser;
                let positionYUser;
                let tamanhoUser;

                if (flag) {
                    let positionUser = username_coordenadas.get(id);
                    positionXUser = positionUser.positionx;
                    positionYUser = positionUser.positiony;
                    tamanhoUser = radius - ((positionUser.nivel) / 10);
                    if (tamanhoUser < 0.2) {
                        tamanhoUser = 0.2;
                    }
                } else {
                    positionXUser = circle.position.x;
                    positionYUser = circle.position.y;
                    tamanhoUser = tamanho;
                }

                //variação em x
                let pontoIntermedioX;
                let pontoIntermedioY;

                //variação em x negativa
                if (positionXUserligado !== positionXUser && positionYUserligado === positionYUser && positionXUserligado > positionXUser) {
                    pontoIntermedioX = ((positionXUserligado - tamanhoUserLigado) + (positionXUser + tamanhoUser)) / 2;
                    pontoIntermedioY = (positionYUserligado + positionYUser) / 2;
                    points = [new THREE.Vector3(positionXUserligado - tamanhoUserLigado, positionYUserligado, 0.0), new THREE.Vector3(positionXUser + tamanhoUser, positionYUser, 0.0)];
                    connectionSize(positionXUserligado - tamanhoUserLigado, positionYUserligado, positionXUser + tamanhoUser, positionYUser, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                }

                //variação em x positiva
                if (positionXUserligado !== positionXUser && positionYUserligado === positionYUser && positionXUserligado < positionXUser) {
                    pontoIntermedioX = ((positionXUserligado + tamanhoUserLigado) + (positionXUser - tamanhoUser)) / 2;
                    pontoIntermedioY = (positionYUserligado + positionYUser) / 2;
                    points = [new THREE.Vector3(positionXUserligado + tamanhoUserLigado, positionYUserligado, 0.0), new THREE.Vector3(positionXUser - tamanhoUser, positionYUser, 0.0)];
                    connectionSize(positionXUserligado + tamanhoUserLigado, positionYUserligado, positionXUser - tamanhoUser, positionYUser, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                }

                //variação em y

                //variação em y negativa
                if (positionYUserligado !== positionYUser && positionXUserligado === positionXUser && positionYUserligado > positionYUser) {
                    pontoIntermedioX = ((positionXUserligado) + (positionXUser)) / 2;
                    pontoIntermedioY = ((positionYUserligado - tamanhoUserLigado) + (positionYUser + tamanhoUser)) / 2;
                    points = [new THREE.Vector3(positionXUserligado, positionYUserligado - tamanhoUserLigado, 0.0), new THREE.Vector3(positionXUser, positionYUser + tamanhoUser, 0.0)];
                    connectionSize(positionXUserligado, positionYUserligado - tamanhoUserLigado, positionXUser, positionYUser + tamanhoUser, points, 2, myMap.get('' + niveis)[i].forcaLigacao);
                }

                //variação em y positiva
                if (positionYUserligado !== positionYUser && positionXUserligado === positionXUser && positionYUserligado < positionYUser) {
                    pontoIntermedioX = ((positionXUserligado) + (positionXUser)) / 2;
                    pontoIntermedioY = ((positionYUserligado + tamanhoUserLigado) + (positionYUser - tamanhoUser)) / 2;
                    points = [new THREE.Vector3(positionXUserligado, positionYUserligado + tamanhoUserLigado, 0.0), new THREE.Vector3(positionXUser, positionYUser - tamanhoUser, 0.0)];
                    connectionSize(positionXUserligado, positionYUserligado + tamanhoUserLigado, positionXUser, positionYUser - tamanhoUser, points, 2, myMap.get('' + niveis)[i].forcaLigacao);
                }

                //variação em x e em y
                if (positionYUserligado !== positionYUser && positionXUserligado !== positionXUser) {
                    let distanciaEntreDoisPontos = Math.sqrt(Math.pow(positionXUserligado - positionXUser, 2) + Math.pow(positionYUserligado - positionYUser, 2));
                    let valorTeoremaTales = distanciaEntreDoisPontos / tamanhoUserLigado;
                    let x = (Math.abs(positionXUser - positionXUserligado) / valorTeoremaTales);
                    let y = (Math.abs(positionYUser - positionYUserligado) / valorTeoremaTales);
                    let distanciaEntreDoisPontos2 = distanciaEntreDoisPontos - tamanhoUser;
                    let valorTeoremaTales2 = distanciaEntreDoisPontos2 / tamanhoUserLigado;
                    let x1 = x * valorTeoremaTales2;
                    let y1 = y * valorTeoremaTales2;
                    // x > e y >
                    if (positionYUser > positionYUserligado && positionXUser > positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado + x) + (positionXUserligado + x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado + y) + (positionYUserligado + y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado + x, positionYUserligado + y, 0.0), new THREE.Vector3(positionXUserligado + x1, positionYUserligado + y1, 0.0)];
                        connectionSize(positionXUserligado + x, positionYUserligado + y, positionXUserligado + x1, positionYUserligado + y1, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                    }

                    // x < e y >
                    if (positionYUser > positionYUserligado && positionXUser < positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado - x) + (positionXUserligado - x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado + y) + (positionYUserligado + y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado - x, positionYUserligado + y, 0.0), new THREE.Vector3(positionXUserligado - x1, positionYUserligado + y1, 0.0)];
                        connectionSize(positionXUserligado - x, positionYUserligado + y, positionXUserligado - x1, positionYUserligado + y1, points, 3, myMap.get('' + niveis)[i].forcaLigacao);
                    }

                    // x < e y <
                    if (positionYUser < positionYUserligado && positionXUser < positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado - x) + (positionXUserligado - x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado - y) + (positionYUserligado + y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado - x, positionYUserligado - y, 0.0), new THREE.Vector3(positionXUserligado - x1, positionYUserligado - y1, 0.0)];
                        connectionSize(positionXUserligado - x, positionYUserligado - y, positionXUserligado - x1, positionYUserligado - y1, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                    }

                    // x > e y <
                    if (positionYUser < positionYUserligado && positionXUser > positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado + x) + (positionXUserligado + x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado - y) + (positionYUserligado - y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado + x, positionYUserligado - y, 0.0), new THREE.Vector3(positionXUserligado + x1, positionYUserligado - y1, 0.0)];
                        connectionSize(positionXUserligado + x, positionYUserligado - y, positionXUserligado + x1, positionYUserligado - y1, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                    }
                }

                let geometryLine = new THREE.BufferGeometry().setFromPoints(points);
                let materialLine = new THREE.LineBasicMaterial({color: 'OrangeRed'});
                let line = new THREE.LineSegments(geometryLine, materialLine);
                scene.add(line);


            }

            raioAtual = raioMinimo;
        }

        function connectionSize(x1, y1, x2, y2, points, option, strength) {
            for (let i = 0; i < strength * 7; i++) {
                if (option === 1) {
                    points.push(new THREE.Vector3(x1, y1 + (0.0001 * i), 0.0), new THREE.Vector3(x2, y2 + (0.0001 * i), 0.0));
                }
                if (option === 2) {
                    points.push(new THREE.Vector3(x1 + (0.0001 * i), y1, 0.0), new THREE.Vector3(x2 + (0.0001 * i), y2, 0.0));
                }
                if (option === 3) {
                    points.push(new THREE.Vector3(x1 + (0.0001 * i), y1 + (0.0001 * i), 0.0), new THREE.Vector3(x2 + (0.0001 * i), y2 + (0.0001 * i), 0.0));
                }
            }
        }

        camera.position.z = 40
        camera.position.y = 4
        renderer.setClearColor( 0x000000, 0 );
        renderer.setSize(width, height)


        const renderScene1 = () => {
            let SCREEN_W, SCREEN_H;
            SCREEN_W = window.innerWidth;
            SCREEN_H = window.innerHeight;

            let left,bottom,width,height;

            left = -0.27*SCREEN_W ; bottom =-0.25*SCREEN_H  ; width = 0.8*SCREEN_W-2; height = SCREEN_H-100;
            renderer.setViewport( left, bottom, width, height );
            renderer.render (scene,camera);
            renderer.clearDepth();


        }

        const handleResize = () => {
            width = mount.current.clientWidth
            height = mount.current.clientHeight
            renderer.setSize(width, height)

            camera.aspect = width / height
            camera.updateProjectionMatrix()
            renderScene1()
        }

        const animate = () => {

            renderScene1()
            frameId = window.requestAnimationFrame(animate)
        }

        const start = () => {
            if (!frameId) {
                frameId = requestAnimationFrame(animate)
            }
        }

        const stop = () => {
            cancelAnimationFrame(frameId)
            frameId = null
        }

        mount.current.appendChild(renderer.domElement)
        window.addEventListener('resize', handleResize)
        start()

        return () => {
            stop()
            window.removeEventListener('resize', handleResize)
            mount.current.removeChild(renderer.domElement)

            scene.remove(circle)
            geometry.dispose()
            material.dispose()
        }
    }, [])

    return (
            <div className="vis2" ref={mount} onClick={() => setAnimating(!isAnimating)}>
                <div/>
            </div>

    )
}

export default Minimap;